using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using System;

namespace Parkeringshuset.BusinessLogic.Tests
{
    [TestFixture()]
    public class ParkingMeterLogicTests
    {
        //private static Setup()
        //{
        //    DateTime dT1 = new DateTime(2021, 12, 01, 12, 00, 00);
        //}

        [TestCase("2021-12-01T12:00:00", "2021-12-02T12:00:00", 270)]
        [Test()]
        public void CalculateCostTest_ShouldBeEqual_WhenGivenCorrectValues(
            DateTime checkIn,
            DateTime checkOut,
            int expected)
        {
            var result = CalculateCostLogic.Cost(checkIn, checkOut);

            Assert.AreEqual(expected, result);
        }

        [Test()]
        public void CalculateCostTest_()
        {
            DateTime checkIn = new DateTime(2021, 12, 01, 12, 00, 00);
            DateTime checkOut = new DateTime(2021, 12, 02, 15, 00, 00);
            var result = CalculateCostLogic.Cost(checkIn, checkOut);

            Assert.AreEqual(270, result);
        }
    }
}