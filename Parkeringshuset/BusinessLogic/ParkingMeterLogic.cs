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



        public bool CheckOut (string regNr, string cardInfo, string CSV)
        {
            if (IsCardCredentialsValid(cardInfo, CSV))
            {
                CalculateCost();



                //if(Pm.CheckOut(regNr))
                //{
                //    return true;
                //}

            }
            return false;


            //Betalning. om ja =>
            // checkout mot controller
            // om nej =>
            // feedback på betalning ej ok.
            // 

        }

        public double CalculateCost()
        {
            //1.  00-06 = 5 kr    0.08
            //2.  07-12 = 10 kr   0.16
            //3.  13-17. = 20 kr   0.33   60 
            //4.  18-24 = 10 kr   0.17

            // checkintime  15.00    60 kr
            // checkouttime  20.00   20 kr 
            // 
            var checkOut = new DateTime(2021, 12, 03, 09, 00, 00);   // checkout. 
            DateTime incheck = new DateTime(2021, 12, 02, 19, 00, 00);  // checkint

            var totalTime = checkOut - incheck;

            var NumberOfDays = (int)totalTime.TotalDays;

            if(NumberOfDays < 1 && incheck.Date != checkOut.Date)
            {
                NumberOfDays = 1;
            }


            var counterMorningMinutes = 0;
            var counterLunchMinutes = 0;
            var counterEvningMinutes = 0;
            var counterNightMinutes = 0;
            


            DateTime morningTime = new DateTime(2021,01,01,06,00,00);
            DateTime lunchTime = new DateTime(2021,01,01,12,00,00);
            DateTime EvningTime = new DateTime(2021, 01, 01, 18, 00, 00);
            DateTime NightTime = new DateTime(2021,01,01,23,59,00);


            for (int i = 0; i <= NumberOfDays; i++)
            {
                if(incheck.TimeOfDay < morningTime.TimeOfDay)  // 00-05.59
                {
                    totalTime = checkOut - incheck;
                    TimeSpan totalHoursInThisCategory = default;
                    if (totalTime.TotalMinutes < 360)
                    {
                        totalHoursInThisCategory = totalTime;

                    }
                    else
                    {
                        totalHoursInThisCategory = morningTime.TimeOfDay - incheck.TimeOfDay;
                    }
                    counterMorningMinutes += (int)totalHoursInThisCategory.TotalMinutes;
                    incheck += totalHoursInThisCategory;
                }

                if (incheck.TimeOfDay >= morningTime.TimeOfDay && incheck.TimeOfDay < lunchTime.TimeOfDay) // 06-12.59
                {
                    totalTime = checkOut - incheck;
                    TimeSpan totalHoursInThisCategory = default;
                    if (totalTime.TotalMinutes < 360)
                    {
                        totalHoursInThisCategory = totalTime;

                    }
                    else
                    {
                        totalHoursInThisCategory = lunchTime.TimeOfDay - incheck.TimeOfDay;
                    }

                    counterLunchMinutes += (int)totalHoursInThisCategory.TotalMinutes;
                    incheck += totalHoursInThisCategory;
                    
                }

                if (incheck.TimeOfDay >= lunchTime.TimeOfDay && incheck.TimeOfDay < EvningTime.TimeOfDay) //13-17.59
                {
                    totalTime = checkOut - incheck;
                    TimeSpan totalHoursInThisCategory = default;
                    if (totalTime.TotalMinutes < 360)
                    {
                        totalHoursInThisCategory = totalTime;
                        
                    }
                    else
                    {
                        totalHoursInThisCategory = EvningTime.TimeOfDay - incheck.TimeOfDay;
                    }


                    counterEvningMinutes += (int)totalHoursInThisCategory.TotalMinutes;
                    incheck += totalHoursInThisCategory;
                    
                }

                if (incheck.TimeOfDay >= EvningTime.TimeOfDay)   //18-23.59
                {
                    totalTime = checkOut - incheck;
                    TimeSpan totalHoursInThisCategory = default;
                    if (totalTime.TotalMinutes < 360)
                    {
                        totalHoursInThisCategory = totalTime;

                    }
                    else
                    {
                        totalHoursInThisCategory = NightTime.TimeOfDay - incheck.TimeOfDay;  
                    }

                    counterNightMinutes += (int)totalHoursInThisCategory.TotalMinutes;
                    incheck += totalHoursInThisCategory;
                    incheck = incheck.AddMinutes(1);
                }


            }

           
            var test = (counterNightMinutes * 0.166666666); 

            var sum = (counterMorningMinutes * 0.0833333333) + 
                (counterLunchMinutes * 0.166666666) +
                (counterEvningMinutes * 0.3333333333) + 
                (counterNightMinutes * 0.166666666);

            return Math.Round(sum);



            //double sum = 0;
            //var resut = temp2 - temp;  // 4 h. 

            //if(temp.TimeOfDay < morningTime.TimeOfDay)
            //{
            //    Console.WriteLine("Morgontid 00-06, 5 kr.");

            //}
            //if (temp.TimeOfDay > morningTime.TimeOfDay && temp.TimeOfDay < lunchTime.TimeOfDay)
            //{
            //    Console.WriteLine("Lunchtid 10 kr. 07-12");
            //}
            //if (temp.TimeOfDay > lunchTime.TimeOfDay && temp.TimeOfDay < EvningTime.TimeOfDay)
            //{
            //    Console.WriteLine("Evning 20 kr, 13-18");
            //    var result = EvningTime.TimeOfDay - temp.TimeOfDay;
            //    int minutesInINt = (int)result.TotalMinutes;
            //    double CostPerMinute = 0.34;
            //    sum += minutesInINt * CostPerMinute;
            //    temp += result;

            //}
            //if (temp.TimeOfDay > EvningTime.TimeOfDay)
            //{
            //    Console.WriteLine("Natt 10 kr, 18-24");
            //    var result = NightTime.TimeOfDay - temp.TimeOfDay;
            //    int minutesInINt = (int)result.TotalMinutes;
            //    double CostPerMinute = 0.34;
            //    sum += minutesInINt * CostPerMinute;
            //    temp -= result;
            //}

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
