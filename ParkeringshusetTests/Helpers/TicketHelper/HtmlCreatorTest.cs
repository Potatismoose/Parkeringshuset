using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Models;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Parkeringshuset.Helpers.TicketHelper.Tests
{
    [Category("UnitTests")]
    [TestFixture()]
    public class TicketHelperTests
    {
        [SetUp]
        public void SetUp()
        {
            HtmlCreator Hc = new();
            Hc.CreateHtmlBoilerPlateCode();
        }
        [TearDown]
        public void TearDown()
        {
            string fileName = "ticket.html";
            string fullPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory) + @"/" + 
                fileName;
            File.Delete(fullPath);
            fileName = "parkingTicket.pdf";
            fullPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory) + @"/" + fileName;
            File.Delete(fullPath);
        }

        [Test()]
        public void InsertTicketInformationInHtmlFile_InsertTicketWithoutValues_ExpectsFalse()
        {
            //Arrange
            PTicket ticket = new() { Type = null, Vehicle = null };
            //Act
            HtmlCreator Hc = new();
            var result = Hc.InsertTicketInformationInHtmlFile(ticket);
            //Assert
            Assert.That(result, Is.False);
        }

        [Test()]
        public void InsertTicketInformationInHtmlFile_InsertValidTicket_ExpectsTrue()
        {
            //Arrange
            PTicket ticket = new() { 
                CheckedInTime = DateTime.Now,
                Type = new PType { Name = "Regular" }, 
                Vehicle = new Vehicle { RegistrationNumber = "OLL069"} 
            };
            //Act
            HtmlCreator Hc = new();
            var result = Hc.InsertTicketInformationInHtmlFile(ticket);
            //Assert
            Assert.That(result, Is.True);
        }
    }
}