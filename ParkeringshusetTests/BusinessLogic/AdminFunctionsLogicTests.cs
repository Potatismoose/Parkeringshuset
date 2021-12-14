using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Models;

namespace Parkeringshuset.BusinessLogic.Tests
{
    [TestFixture()]
    public class AdminFunctionsLogicTests
    {
        AdminFunctionsLogic AC = new();
        Admin Admin;
        [SetUp]
        public void Setup()
        {
            Admin = new Admin() { Username = "Test", Password = "test123" };
        }
        [TearDown]
        public void TearDown()
        {
            Admin = null;
        }

        [Test()]
        public void ParkingSportsPopularityLastMonthTest()
        {
            AC.ParkingSpotsPopularity(Admin);

            Assert.Fail();
        }

        [Test()]
        public void BestCustomerTest()
        {
            var Customers = AC.GetCustomerAndMoneySpent(Admin);
            Assert.IsTrue(Customers[0].Item2 == "ABO167");
        }
    }
}