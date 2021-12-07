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

      
    }
}