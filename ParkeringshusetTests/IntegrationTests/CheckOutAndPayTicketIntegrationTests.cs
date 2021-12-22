namespace ParkeringshusetTests.IntegrationTests
{
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Parkeringshuset.BusinessLogic;
    using Parkeringshuset.Data;
    using Parkeringshuset.Models;
    using System.Linq;

    [Category("IntegrationTests")]
    [TestFixture]
    public class CheckOutAndPayTicketIntegrationTests
    {
        private PTicket _ticket;
        private CreditCard _creditCard;
        private ParkingTicketController _tC;
        private PaymentLogic _pL;
        private string _regNr;
        private string _pType;
        private ParkeringGarageContext db = new();

        [SetUp]
        public void Setup()
        {
            _tC = new();
            _pL = new();
            _regNr = "gha238";
            _pType = "Electric";
            _creditCard = new();
            _creditCard.Number = "1234123412341234";
            _creditCard.CSV = "123";
            _tC.CreateTicket(_regNr, _pType);
            _ticket = _tC.GetActiveTicket(_regNr);
        }

        [TearDown]
        public void CleanUp()
        {
            _regNr = "";
            _pType = "";
            _tC = null;
            _ticket = null;
            RemoveLastEntry();
        }

        //MÅSTE RENSA DATABASEN FRÅN SISTA TICKET MELLAN VARJE RUN
        [Test]
        public void IntegrationTest_AssertCheckout_AssertPayment()
        {
            //Check if ticket is active before passing _ticket to Checkout()
            Assert.IsTrue(_ticket.isActice);

            //Check if Checkout is successful
            var checkOutResult = _tC.CheckOut(_ticket);
            Assert.IsTrue(checkOutResult);

            //Check if ticket.IsActive=false after _ticket is passed to CheckOut()
            Assert.IsFalse(_ticket.isActice);

            //Check if ticket is paid after _ticket is passed to Payment()
            _ticket = _pL.Payment(_creditCard, _ticket);
            Assert.IsTrue(_ticket.IsPaid);
        }

        //TODO:GÖRA KLART DEN HÄR METODEN
        private void RemoveLastEntry()
        {
            var ticketToRemove = db.Ptickets.Include(y => y.Vehicle).FirstOrDefault(x => x.Vehicle.RegistrationNumber == "gha238");
            db.Ptickets.Remove(ticketToRemove);
            db.SaveChanges();
        }
    }
}