namespace Parkeringshuset.BusinessLogic
{
    using Parkeringshuset.Controllers;
    using Parkeringshuset.Helper;
    using Parkeringshuset.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ParkingMeterLogic
    {
        private ParkingTicketController Pm = new();
        private ParkingTypeController Pt = new();

        public bool CheckIn(string regNr, string pType)
        {
            //if (Pm.IsMonthly(regNr))
            //{
            //The customer has an active Monthleyticket
            //return true;

            //}

            if (Pt?.ReadFreeSpots(pType) > 0)
            {
                //Om vi kopplar på API mot Transportstyrelsen, kör den checken här!

                //if(Pm?.CreateTicket(regNr))
                //{
                DisplayHelper.DisplayGreen("Ticket is activated. Welcome!");
                return true;
                //}
                //else
                //{
                //DisplayHelper.DisplayRed("Check in failed, try again or contact our support");
                //return false;
                //}
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
                var ticket.Cost = CalculateCost(ticket.CheckedIn, timeOfCheckOut);

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

        //public bool IsCarParkedInGarageAlreadyOrMonthly()
        //        {
        // om det finns reg nr i vehicle = bilen har köpt har en biljett en gång i tiden.
        // Är det en aktiv checkintime men ingen checkouttid  => då finns bilen i garaget. return true om bilen är inne eller är monthly => ska inte betala!

        //            foreach (var parkingmeter in ParkingMeters)
        //            {
        //                List<ParkingTicketController> activeTicket = new();
        //                if (parkingmeter.GetActiveTickets() != null)
        //                {
        //                    foreach (var ticket in parkingmeter.GetActiveTickets())
        //                    {
        //                        if (ticket.IsSameVehicle(vehicle))
        //                        {
        //                            return true;
        //                        }
        //                    }
        //                }
        //            }

        //2. Checkout

        //3. PrintTicket

        //.4 Payment(int cardnr) sätt så ispayed = true.
    }
}