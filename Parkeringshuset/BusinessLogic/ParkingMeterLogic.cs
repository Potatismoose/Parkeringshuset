namespace Parkeringshuset.BusinessLogic
{
    using Parkeringshuset.Controllers;
    using Parkeringshuset.Helper;
    using Parkeringshuset.Helpers.TicketHelper;
    using Parkeringshuset.Models;
    using System;

    public class ParkingMeterLogic
    {
        private ParkingTicketController Pm = new();
        private ParkingTypeController Pt = new();

        /// <summary>
        /// Checks if the car have a Monthly Ticket. 
        /// </summary>
        /// <param name="regNr">Registration number of the car.</param>
        /// <returns>True if there is a monthly ticket. False if not.</returns>
        public bool isMonthly(string regNr)
        {
            var ticket = Pm.GetActiveTicket(regNr);
            if (Pm.IsMonthly(ticket))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if the car is already in parked in the garage.
        /// </summary>
        /// <param name="regNr"></param>
        /// <returns>True if user shall checkout, false if user shall check in.</returns>
        public bool isCarParked(string regNr)
        {
            var ticket = Pm.GetActiveTicket(regNr);
            if (Pm.IsTicketActive(ticket))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks in a car in the system and is creating a ticket if there is spots free. 
        /// </summary>
        /// <param name="regNr">Registration number of the car.</param>
        /// <param name="pType">Parking type for the car.</param>
        /// <returns>True if evertything succeeded, false if not.</returns>
        public bool CheckIn(string regNr, string pType)
        {

            if (Pt.ReadFreeSpots(pType) > 0)
            {

                if (Pm.CreateTicket(regNr, pType))
                {
                    var ticket = Pm.GetActiveTicket(regNr);

                    if (ticket is not null)
                    {
                        DisplayHelper.DisplayGreen("Ticket is activated. Welcome!");
                        PrintingHelper.PhysicalTicketCreationAndPrintout(ticket);       // TODO: Need to add +1 to UsedSpots i Ptypes table.       
                        return true;
                    }
                }
            }

            else
            {
                DisplayHelper.DisplayRed("There is no available parking spots for this type.");
                return false;
            }

            DisplayHelper.DisplayRed("Check in failed, try again or contact our support");
            return false;
        }

        /// <summary>
        /// Checks out a c
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="cardInfo"></param>
        /// <param name="CSV"></param>
        public bool CheckOut(string regNr, string cardInfo, string CSV)
        {
            var ticket = Pm.GetActiveTicket(regNr);
            if (ticket is not null)
            {
                Payment(regNr, cardInfo, CSV, ticket);

                if (Pm.CheckOut(ticket))
                {
                    return true;
                }
            }

            DisplayHelper.DisplayRed("The check out was not successful. Please contact support.");
            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="cardInfo"></param>
        /// <param name="CSV"></param>
        /// <param name="ticket"></param>
        private static void Payment(string regNr, string cardInfo, string CSV, PTicket ticket)
        {
            DateTime timeOfCheckOut = DateTime.Now;
            ticket.Cost = CalculateCostLogic.Cost(ticket.CheckedInTime, timeOfCheckOut);

            if (IsCardCredentialsValid(cardInfo, CSV))
            {
                ticket.IsPaid = true;         // TODO: Needs to be able to update ticket to database through controller.
                DisplayHelper.DisplayGreen($"The transaction was successful!\nThe total fee was: {ticket.Cost} kr");
            }
            else
            {
                DisplayHelper.DisplayRed($"The card credentials was invalid! An invoice is sent to the adress of the car with registration number: {regNr}");
            }
        }

        /// <summary>
        /// Checks the validation of credit card. 
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <param name="CSV"></param>
        /// <returns></returns>
        private static bool IsCardCredentialsValid(string cardInfo, string CSV)
        {
            if (cardInfo?.Length == 16 && CSV?.Length == 3)
            {
                if (int.TryParse(cardInfo, out int checkedCardInfo))
                {
                    DisplayHelper.DisplayGreen("Payment is done");
                    return true;
                }
            }
            return false;
        }
    }
}