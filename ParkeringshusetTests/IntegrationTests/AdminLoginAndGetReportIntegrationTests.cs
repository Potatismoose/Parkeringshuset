namespace ParkeringshusetTests.IntegrationTests
{
    using NUnit.Framework;
    using Parkeringshuset.BusinessLogic;
    using Parkeringshuset.Controllers;
    using Parkeringshuset.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Category("IntegrationTests")]
    [TestFixture()]
    public class AdminLoginAndGetReportIntegrationTests
    {
        private LoginController _lC;
        private AdminFunctionsLogic _aFL;
        private Admin _admin;
        private string _username;
        private string _password;
        private DateTime _from;
        private DateTime _to;

        [SetUp]
        public void Setup()
        {
            _lC = new();
            _aFL = new();

            _username = "admin";
            _password = "admin123";

            _from = new(2021, 12, 14);
            _to = new(2021, 12, 17);
        }

        [Test]
        public void IntegrationTest_AdminLoginAndGetReport()
        {
            //Check if _admin object is null before passing to LoginReturnAdmin()
            Assert.IsNull(_admin);

            //Check if _admin object is not null before passing to LoginReturnAdmin()
            _admin = _lC.LoginReturnAdmin(_username, _password);
            Assert.IsNotNull(_admin);

            //Check in database how many tickets exist between _from and _to
            int expected = 26;
            int actual;
            actual = _aFL.SoldTicketsBetweenSpecificDates(_admin, _from, _to);

            Assert.AreEqual(expected, actual);
        }
    }
}