using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Models;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Parkeringshuset.Helpers.TicketHelper.Tests
{
    [TestFixture()]
    public class HtmlCreatorTest
    {

        [SetUp]
        public void SetUp()
        {
            HtmlCreator.CreateHtmlBoilerPlateCode();
        }
        [TearDown]
        public void TearDown()
        {
            string fileName = "ticket.html";
            string fullPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\" + fileName;
            File.Delete(fullPath);
        }

        [Test()]
        public void InsertTicketInformationInHtmlFile_InsertTicketWithoutValues_ExpectsFalse()
        {
            //Arrange
            PTicket ticket = new() { Type = null, Vehicle = null };
            //Act
            
            var result = HtmlCreator.InsertTicketInformationInHtmlFile(ticket);
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
            
            var result = HtmlCreator.InsertTicketInformationInHtmlFile(ticket);
            //Assert
            Assert.That(result, Is.True);
        }

    }
}