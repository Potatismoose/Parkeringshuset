namespace Parkeringshuset.BusinessLogic
{
    using Parkeringshuset.Controllers;
    using Parkeringshuset.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using Parkeringshuset.Helpers.Email;

    public class AdminFunctionsLogic
    {
        private AdminController Controller = new();
        private int TotalIncome;
        private int CounterForNotPaidBills;
        private List<PTicket> Tickets;

        /// <summary>
        /// Get the occupation of all parking types for the last month. 
        /// </summary>
        /// <param name="admin">To verify that you are admin.</param>
        /// <returns>A list of tupes, the string is the name of the parking type and the int the number of cars that used it last 30 days.</returns>
        public List<(int, String)> ParkingSpotsPopularity(Admin admin)
        {
            if (admin is not null)
            {
                List<(int, String)> occupationLastMonth = new();
                var daysAgo30 = DateTime.Now.AddDays(-30);
                var today = DateTime.Now;
                var tickets = Controller.GetTicketForDate(daysAgo30, today).GroupBy(u => u.Type).ToList();

                foreach (var ticket in tickets)
                {
                    occupationLastMonth.Add((ticket.Count(), ticket.Key.Name));
                }
                occupationLastMonth.Sort();
                occupationLastMonth.Reverse();
                //SendEmail.ParkingSpots(occupationLastMonth,
                //"Popularity of Parking Spots", admin.Email);
                return occupationLastMonth;
            }
            else return null;      
        }

        /// <summary>
        /// Get how many ticket that was sold during choosen dates. 
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="from">start date.</param>
        /// <param name="to">end date.</param>
        /// <returns>An Integer that represent number of sold tickets.</returns>
        public int SoldTicketsBetweenSpecificDates(Admin admin, DateTime from, DateTime to)
        {
           return Controller.GetTicketForDate(from, to).Count();
            

        }
        /// <summary>
        /// Sends a report to admin by email of a total sum by tickets that been sold during choosen dates. 
        /// The report also includes unpaid bills.. 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public bool Revenue(Admin admin, DateTime startDate, DateTime endDate)
        {
            Tickets = Controller.GetTicketForDate(startDate, endDate);
          
            SumOfIncome(Tickets);

         
          try{

                SendEmail.Revenue(TotalIncome, CounterForNotPaidBills,
                "Revenue", startDate, endDate, admin.Email);
            
                return true;
            }
          catch
            {
                return false;
            }
        }

        /// <summary>
        /// Send a revenue report to admin by email. The report includes the income of the year and if there are unpaid bills.
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="year"></param>
        public void Revenue(Admin admin, DateTime year)
        {
            DateTime startDate = new DateTime(year.Year,01,01);
            DateTime endDate = new DateTime((year.Year + 1),12,31);
            Tickets = Controller.GetTicketForDate(startDate, endDate);
            SumOfIncome(Tickets);
            SendEmail.Revenue(TotalIncome, CounterForNotPaidBills,
                $"Years Revenue {year.Year}", startDate, endDate, admin.Email);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public List<(int, string)> GetCustomerAndMoneySpent(Admin admin)
        {
            Tickets = Controller.GetAllTickets();
            var Cars = Tickets.GroupBy(x => x.Vehicle).ToList();
            List<(int, string)> moneySpentOnEveryCar = new();
           

            foreach(var car in Cars)
            {
                int counter = 0;
                foreach (var ticket in car)
                {
                    counter += ticket.Cost;
                }

                moneySpentOnEveryCar.Add((counter,car.Key.RegistrationNumber));
            }

            moneySpentOnEveryCar.Sort();
            moneySpentOnEveryCar.Reverse();     
            return moneySpentOnEveryCar;
        }

        public void comprehensiveReport(Admin admin)
        {

        }
        private void SumOfIncome(List<PTicket> tickets)
        {
            TotalIncome = 0;
            CounterForNotPaidBills = 0;
            foreach (var ticket in tickets)
            {
                TotalIncome += ticket.Cost;

                if (ticket.Cost == 0)
                {
                    CounterForNotPaidBills++;
                }
            }
        }

    }
}