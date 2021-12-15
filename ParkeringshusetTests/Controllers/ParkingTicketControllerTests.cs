using NUnit.Framework;

namespace Parkeringshuset.Models.Tests
{
    [Category("UnitTests")]
    [TestFixture()]
    public class ParkingTicketControllerTests
    {
        ParkingTicketController pTC = new();
        PTicket ticket = null;
        [SetUp]
        public void Setup()
        {
            pTC.CreateTicket("MCD111", "Monthly");
            ticket = pTC.GetActiveTicket("MCD111");
        }
        [TearDown]
        public void TearDown()
        {
            ticket = null;
        }

        [Test()]
        public void IsMonthlyTest()
        {
            // Act
            var result = pTC.IsMonthly(ticket);
            // Assert
            Assert.That(result is true);
        }
    }
}