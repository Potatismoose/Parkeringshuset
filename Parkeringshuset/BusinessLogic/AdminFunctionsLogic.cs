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
        AdminController Controller = new();

        public PType ParkingSportsPopularity(Admin admin)
        {

            return new PType();
        }
        public int SoldTicketsBetweenSpecificDates(Admin admin, DateTime from, DateTime to)
        {
            var tickets = Controller.GetTicketForDate(from, to);
            return tickets.Count;

        }
        public void Revenue(DateTime from, DateTime to)
        {
            var tickets = Controller.GetTicketForDate(from, to);
            var totalIncome = 0;
            var counterForNotPaidBills = 0;
            foreach (var ticket in tickets)
            {
                totalIncome += ticket.Cost;

                if (ticket.Cost == 0)
                {
                    counterForNotPaidBills++;
                }
            }

            SendEmail.SendWithBlazor(totalIncome, counterForNotPaidBills, "Revenue", from, to);

        }
        public void Revenue(Admin admin, DateTime year)
        {

        }
        public Vehicle BestCustomer(Admin admin)
        {
            return new Vehicle();
        }

        public void comprehensiveReport(Admin admin)
        {
            // En månads rapport, 
        }

    }
}