using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using System;
using System.Globalization;

namespace Parkeringshuset.BusinessLogic.Tests
{
    [TestFixture()]
    public class ParkingMeterLogicTests
    {


        [TestCase("2021/05/28 07:00", "2021/05/28 08:00", 10)]
        //[TestCase("2021-12-01T12:00:00", "2021-12-02T1:00:00", 270)]
        //[TestCase("2021-12-01T12:00:00", "2021-12-02T15:00:00", 270)]
        [Test()]
        public void CalculateCostTest_ShouldBeEqual_WhenGivenCorrectValues(
            DateTime checkIn,
            DateTime checkOut,
            int expected)
        {

            // Acording to all documentation, this should work but dosen´t.. haha
           // string format = "yyyy-MM-dd HH:mm:ss";
           //CultureInfo cultureinfo =
           //      CultureInfo.CreateSpecificCulture("en-US");
           // var test = DateTime.ParseExact(checkIn.ToString(),format, cultureinfo);

            var result = CalculateCostLogic.Cost(checkIn, checkOut);

            Assert.AreEqual(expected, result);
        }

    }
}