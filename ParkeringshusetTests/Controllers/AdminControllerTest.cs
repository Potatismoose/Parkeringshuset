using NUnit.Framework;
using Parkeringshuset.Data;
using Parkeringshuset.Models;
using System;
using System.Linq;

namespace Parkeringshuset.Controllers.Tests
{
    [TestFixture()]
    public class AdminControllerTest
    {
        ParkeringGarageContext db = new();

        AdminController AC = new AdminController();
        ParkingTicketController PC = new ();
        [Test()]
        public void GetAllTickets()
        {
           
            var result = AC.GetAllTickets(); 

            if(result.Count == 0)
            {
                PC.CreateTicket("YPW123", ParkingTypesNames.Regular);
            }
            Assert.That(result.Count, Is.AtLeast(2));
        }

        [Test()]
        public void TicketsInARange()
        {

            DateTime from = new DateTime(2021,12,14);
            DateTime to = new DateTime(2021, 12, 14);
            var result = AC.GetTicketForDate(from, to);
            Assert.AreEqual(2, result.Count);
        }

        [Test()]
        public void GetAllActivatedMonthlyTickets()
        {
            var result = AC.GetActiveMonthlyTickets();
            Assert.That(result.Count, Is.AtLeast(1));
        }
    }
}