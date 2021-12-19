namespace Parkeringshuset.BusinessLogic
{
    using Parkeringshuset.Helper;
    using Parkeringshuset.Models;
    using Parkeringshuset.Views;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class PaymentLogic
    {
        private CultureInfo ci = new CultureInfo("sv-SE");
        private TimeSpan TotalTime;
        private DateTime MorningTime = new DateTime(2021, 01, 01, 06, 00, 00);
        private DateTime LunchTime = new DateTime(2021, 01, 01, 12, 00, 00);
        private DateTime EveningTime = new DateTime(2021, 01, 01, 18, 00, 00);
        private DateTime NightTime = new DateTime(2021, 01, 01, 23, 59, 00);
        private const double MorningPricePerMinute = 0.0833333333;
        private const double LunchPricePerMinute = 0.166666666;
        private const double EveningPricePerMinute = 0.3333333333;
        private const double NightPricePerMinute = 0.166666666;
        private int MorningMinsCounter = 0;
        private int LunchMinsCounter = 0;
        private int EveningMinsCounter = 0;
        private int NightMinsCounter = 0;
        private DateTime CheckIn;
        private DateTime CheckOut;

        public int CalculateCost(DateTime checkIn, DateTime checkOut)
        {
            Thread.CurrentThread.CurrentCulture = ci;

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

            return GetSumAndResetCounters();
        }

        private int GetSumAndResetCounters()
        {
            var res = Math.Round(

                 (MorningMinsCounter * MorningPricePerMinute) +
                (LunchMinsCounter * LunchPricePerMinute) +
                (EveningMinsCounter * EveningPricePerMinute) +
                (NightMinsCounter * NightPricePerMinute));

            MorningMinsCounter = 0;
            LunchMinsCounter = 0;
            EveningMinsCounter = 0;
            NightMinsCounter = 0;
            return (int)res;
        }

        private void MinutesInThisTimeSlot(DateTime CategoryTime, ref int counter)
        {
            TotalTime = CheckOut - CheckIn;
            var totalHoursInThisCategory = CategoryTime.TimeOfDay - CheckIn.TimeOfDay;

            if (totalHoursInThisCategory.TotalMinutes > TotalTime.TotalMinutes)
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

        /// <summary>
        /// Handle the payment. Updates the object ticket properties Cost and IsPaid if successfull.
        /// </summary>
        /// <param name="card">CreditCard object.</param>
        /// <param name="ticket">Ticket object.</param>
        /// <returns>a Tuple of an updated ticket and a bool. True if card is valid and false if not.</returns>
        public PTicket Payment(CreditCard card, PTicket ticket)
        {
            ticket.CheckedOutTime = DateTime.Now;
            ticket.Cost = CalculateCost(ticket.CheckedInTime, ticket.CheckedOutTime);

            if (IsCardCredentialsValid(card))
            {
                ticket.IsPaid = true;
                return ticket;
            }
            else
            {
                ticket.IsPaid = false;
                DisplayHelper.DisplayRed("Invalid credentials");
                MainMenu.PressAnyKeyToContinue();
                return ticket;
            }
        }

        /// <summary>
        /// Takes an object of a Card. The properties of the card are strings and if number contains 16 symbols and possible to convert to int the card is valid.
        /// </summary>
        /// <param name="card">Object that have 2 string properties.</param>
        /// <returns></returns>
        public bool IsCardCredentialsValid(CreditCard card)
        {
            if (card?.Number.Length == 16 && card.CSV?.Length == 3)
            {
                if (long.TryParse(card.Number, out long CardValidation) && int.TryParse(card.CSV, out int CSVValidation))
                {
                    return true;
                }
            }
            return false;
        }
    }
}