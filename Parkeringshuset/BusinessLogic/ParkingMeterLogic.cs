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

        public double CalculateCost()
        {
            DateTime checkOut = new DateTime(2021, 12, 03, 09, 00, 00);   // checkout.
            DateTime incheck = new DateTime(2021, 12, 02, 19, 00, 00);  // checkin

            var totalTime = checkOut - incheck;             // get the parkingtime in hours and minutes.
            var NumberOfDays = (int)totalTime.TotalDays;    // if the parking is less then 24 hours but over the night we need to add a day to make the for loop run correctly

            if (NumberOfDays < 1 && incheck.Date != checkOut.Date)
            {
                NumberOfDays = 1;
            }

            int morningMinutes = 0, lunchMinutes = 0, evningMinutes = 0, nightMinutes = 0;   // declaring variabels I need down below.

            DateTime morningTime, lunchTime, evningTime, nightTime;
            GetTimeSlotsInDateTime(out morningTime, out lunchTime, out evningTime, out nightTime);

            for (int i = 0; i <= NumberOfDays; i++)
            {
                if (incheck.TimeOfDay < morningTime.TimeOfDay)  // 00-05.59
                {
                    totalTime = checkOut - incheck;
                    morningMinutes += (int)MinutesInThisTimeSlot(incheck, totalTime, morningTime);
                    incheck = incheck.AddMinutes(MinutesInThisTimeSlot(incheck, totalTime, morningTime));
                }

                if (incheck.TimeOfDay >= morningTime.TimeOfDay && incheck.TimeOfDay < lunchTime.TimeOfDay) // 06-12.59
                {
                    totalTime = checkOut - incheck;
                    lunchMinutes += (int)MinutesInThisTimeSlot(incheck, totalTime, lunchTime);
                    incheck = incheck.AddMinutes(MinutesInThisTimeSlot(incheck, totalTime, lunchTime));
                }

                if (incheck.TimeOfDay >= lunchTime.TimeOfDay && incheck.TimeOfDay < evningTime.TimeOfDay) //13-17.59
                {
                    totalTime = checkOut - incheck;
                    evningMinutes += (int)MinutesInThisTimeSlot(incheck, totalTime, evningTime);
                    incheck = incheck.AddMinutes(MinutesInThisTimeSlot(incheck, totalTime, evningTime));
                }

                if (incheck.TimeOfDay >= evningTime.TimeOfDay)   //18-23.59
                {
                    totalTime = checkOut - incheck;                                                                 // DateTime dosent accept 24:00. there for i set nightime to 23.59.
                    nightMinutes += (int)MinutesInThisTimeSlot(incheck, totalTime, nightTime);                      // and then add an extra minute so it goes in to the mornings IF-statement if car is parked over
                    incheck = incheck.AddMinutes(MinutesInThisTimeSlot(incheck, totalTime, nightTime) + 1);         // the night.
                }
            }

            return Math.Round(
                 (morningMinutes * 0.0833333333) +
                (lunchMinutes * 0.166666666) +
                (evningMinutes * 0.3333333333) +
                (nightMinutes * 0.166666666));
        }

        private static double MinutesInThisTimeSlot(DateTime incheck, TimeSpan totalTime, DateTime CategoryTime)
        {
            TimeSpan totalHoursInThisCategory;
            if (totalTime.TotalMinutes <= 360)
            {
                totalHoursInThisCategory = totalTime;
            }
            else
            {
                totalHoursInThisCategory = CategoryTime.TimeOfDay - incheck.TimeOfDay;
            }

            return totalHoursInThisCategory.TotalMinutes;
        }

        private static void GetTimeSlotsInDateTime(out DateTime morningTime, out DateTime lunchTime, out DateTime EvningTime, out DateTime NightTime)
        {
            morningTime = new DateTime(2021, 01, 01, 06, 00, 00);
            lunchTime = new DateTime(2021, 01, 01, 12, 00, 00);
            EvningTime = new DateTime(2021, 01, 01, 18, 00, 00);
            NightTime = new DateTime(2021, 01, 01, 23, 59, 00);
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