using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using System;
using System.Globalization;
using System.Threading;

namespace Parkeringshuset.BusinessLogic.Tests
{
    [Category("UnitTests")]
    [TestFixture()]
    public class ParkingMeterLogicTests
    {
        private PaymentLogic pl = new();

        [TestCase("2021/05/28 07:00", "2021/05/28 08:00", 10)]
        [TestCase("2021/05/28 07:00", "2021/05/28 13:00", 70)]
        [TestCase("2021/12/01 12:00", "2021/12/02 12:00", 270)]
        [TestCase("2021/12/01 12:00", "2021/12/02 13:00", 290)]
        [Test()]
        public void CalculateCostTest_ShouldBeEqual_WhenGivenCorrectValues(DateTime checkIn, 
            DateTime checkOut, int expected)
        {
            var actual = pl.CalculateCost(checkIn, checkOut);
            Assert.AreEqual(expected, actual);
        }
    }
}