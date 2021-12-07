﻿using NUnit.Framework;
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
        [Test()]
        public void GetAllTickets()
        {
            var result = AC.GetAllTickets(); 
            Assert.IsTrue(result.Count == 8);

        }

        [Test()]
        public void TicketsInARange()
        {
            DateTime from = new DateTime(2021,12,06);
            DateTime to = new DateTime(2021, 12, 10);
            var result = AC.GetTicketForDate(from, to);
            Assert.AreEqual(7, result.Count);

        }
    }
}