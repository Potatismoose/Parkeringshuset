namespace Parkeringshuset.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class CalculateCostLogic
    {
        private static TimeSpan TotalTime;
        private static DateTime MorningTime = new DateTime(2021, 01, 01, 06, 00, 00);
        private static DateTime LunchTime = new DateTime(2021, 01, 01, 12, 00, 00);
        private static DateTime EveningTime = new DateTime(2021, 01, 01, 18, 00, 00);
        private static DateTime NightTime = new DateTime(2021, 01, 01, 23, 59, 00);
        private static int MorningMinsCounter = 0;
        private static int LunchMinsCounter = 0;
        private static int EveningMinsCounter = 0;
        private static int NightMinsCounter = 0;
        private static DateTime CheckIn;
        private static DateTime CheckOut;

        public static int Cost(DateTime checkIn, DateTime checkOut)
        {
            //this works if using an EN OS. 

            //string format = "M/d/yyyy h:mm:ss tt";
            //CheckIn = DateTime.ParseExact(checkIn.ToString(), format, CultureInfo.InvariantCulture);
            //CheckOut = DateTime.ParseExact(checkOut.ToString(), format, CultureInfo.InvariantCulture);

            CheckIn = checkIn;
            CheckOut = checkOut;

            var tempDayCheck = CheckOut.Date - CheckIn.Date;
            var NumberOfDays = tempDayCheck.TotalDays;

            for (int i = 0; i <= NumberOfDays; i++)
            {
                if (CheckIn.TimeOfDay < MorningTime.TimeOfDay)  // 00-05.59
                {
                    MinutesInThisTimeSlot(MorningTime, ref MorningMinsCounter);
                }

                if (CheckIn.TimeOfDay >= MorningTime.TimeOfDay && CheckIn.TimeOfDay < LunchTime.TimeOfDay) // 06-11.59
                {
                    MinutesInThisTimeSlot(LunchTime, ref LunchMinsCounter);
                }

                if (CheckIn.TimeOfDay >= LunchTime.TimeOfDay && CheckIn.TimeOfDay < EveningTime.TimeOfDay) //12-17.59
                {
                    MinutesInThisTimeSlot(EveningTime, ref EveningMinsCounter);
                }

                if (CheckIn.TimeOfDay >= EveningTime.TimeOfDay)   //18-23.59
                {
                    MinutesInThisTimeSlot(NightTime, ref NightMinsCounter);
                    NightMinsCounter++;                                                      // DateTime dosent accept 24:00. there for i set nightime to 23.59.
                    CheckIn = CheckIn.AddMinutes(1);                                         // and then add an extra minute so it goes in to the mornings IF-statement if car is parked over
                }                                                                            // the night.
            }
            return (int)Math.Round(
                 (MorningMinsCounter * 0.0833333333) +
                (LunchMinsCounter * 0.166666666) +
                (EveningMinsCounter * 0.3333333333) +
                (NightMinsCounter * 0.166666666));
        }

        private static void MinutesInThisTimeSlot(DateTime CategoryTime, ref int counter)
        {
            TimeSpan totalHoursInThisCategory;
            TotalTime = CheckOut - CheckIn;
            if (TotalTime.TotalMinutes <= 360)
            {
                totalHoursInThisCategory = TotalTime;
            }
            else
            {
                totalHoursInThisCategory = CategoryTime.TimeOfDay - CheckIn.TimeOfDay;
            }

            counter += (int)totalHoursInThisCategory.TotalMinutes;

            CheckIn = CheckIn.AddMinutes(totalHoursInThisCategory.TotalMinutes);
        }
    }
}