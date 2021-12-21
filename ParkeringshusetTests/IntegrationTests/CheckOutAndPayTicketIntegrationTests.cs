namespace ParkeringshusetTests.IntegrationTests
{
    using NUnit.Framework;
    using Parkeringshuset.BusinessLogic;
    using Parkeringshuset.Models;

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

        [SetUp]
        public void Setup()
        {
            _tC = new();
            _pL = new();
            _regNr = "qwe123";
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
        }

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
    }
}