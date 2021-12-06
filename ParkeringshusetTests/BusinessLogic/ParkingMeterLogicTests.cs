using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using System;

namespace Parkeringshuset.BusinessLogic.Tests
{
    [TestFixture()]
    public class ParkingMeterLogicTests
    {
        [Test()]
        public void CalculateCost()
        {
            DateTime checkIn = new DateTime(2021, 12, 01, 12, 00, 00);
            DateTime checkOut = new DateTime(2021, 12, 02, 12, 00, 00);
            var result = CalculateCostLogic.Cost(checkIn, checkOut);

            Assert.AreEqual(270, result);
        }
    }
}