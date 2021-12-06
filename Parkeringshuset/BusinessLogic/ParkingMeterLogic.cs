namespace Parkeringshuset.BusinessLogic
{
    using Parkeringshuset.Controllers;
    using Parkeringshuset.Helper;
    using Parkeringshuset.Models;
    using System;

    public class ParkingMeterLogic
    {
        private ParkingTicketController Pm = new();
        private ParkingTypeController Pt = new();

        public bool CheckIn(string regNr, string pType)
        {
            var ticket = Pm.GetActiveTicket(regNr);

            if (Pm.IsMonthly(ticket))
            {
                // The customer has an active Monthleyticket
                return true;
            }

            if (Pt?.ReadFreeSpots(pType) > 0)
            {
                //  Om vi kopplar på API mot Transportstyrelsen, kör den checken här!

                if (Pm.CreateTicket(regNr, pType))
                {
                    DisplayHelper.DisplayGreen("Ticket is activated. Welcome!");
                    return true;
                }
                else
                {
                    DisplayHelper.DisplayRed("Check in failed, try again or contact our support");
                    return false;
                }
            }
            else
            {
                DisplayHelper.DisplayRed("There is no available parking spots for this type.");
                return false;
            }
        }

        public void CheckOut(string regNr, string cardInfo, string CSV)
        {
            var ticket = Pm.GetActiveTicket(regNr);
            if (ticket is not null)
            {
                DateTime timeOfCheckOut = DateTime.Now;
                ticket.Cost = CalculateCostLogic.Cost(ticket.CheckedInTime, timeOfCheckOut);

                if (IsCardCredentialsValid(cardInfo, CSV))
                {
                    ticket.IsPaid = true;
                    DisplayHelper.DisplayGreen($"The transaction was successful!\nThe total fee was: {ticket.Cost} kr");
                }
                else
                {
                    DisplayHelper.DisplayRed($"The card credentials was invalid! An invoice is sent to the adress of the car with registration number: {regNr}");
                }
                if (!Pm.CheckOut(ticket))
                {
                    DisplayHelper.DisplayRed("The check out was not successful. Please contact support.");
                }
            }
            else
            {
                DisplayHelper.DisplayRed("Ticket was not found!");
            }
        }

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