using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Models;
using Parkeringshuset.Helpers.TicketHelper;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Parkeringshuset.Helpers.TicketHelper.Tests
{
    [Category("IntegrationTests")]
    [TestFixture()]
    public class TicketHelperIntegrationTests
    {
        PTicket Ticket = null;
        
        [SetUp]
        public void Setup()
        {
            Ticket = new PTicket()
            {
                Vehicle = new Vehicle()
                {
                    RegistrationNumber = "ZAZ112"
                },
                Type = new PType()
                {
                    Name = "Regular"
                },
                CheckedInTime = DateTime.Now
            };
        }

        [Test()]
        public void IntegrationTest_TestWorkingFlow_ExpectTrue()
        {
            HtmlCreator Hc = new();
            var result = Hc.CreateHtmlBoilerPlateCode();
            Assert.IsTrue(result);
            result = Hc.InsertTicketInformationInHtmlFile(Ticket);
            Assert.IsTrue(result);
            result = PdfCreator.CreatePdfFromHtmlFile();
            Assert.IsTrue(result);
        }
    }
}