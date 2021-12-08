using NUnit.Framework;
using Parkeringshuset.BusinessLogic;
using System;
using System.Globalization;

namespace Parkeringshuset.BusinessLogic.Tests
{
    [TestFixture()]
    public class ParkingMeterLogicTests
    {
        private PaymentLogic pl = new();

        [TestCase("2021/05/28 07:00", "2021/05/28 08:00", 10)]
        [TestCase("2021/05/28 07:00", "2021/05/28 13:00", 70)]
        [TestCase("2021/12/01 12:00", "2021/12/02 12:00", 270)]
        [TestCase("2021/12/01 12:00", "2021/12/02 13:00", 290)]
        [TestCase("2021/12/01 12:00", "2021/12/02 15:00", 270)]
        [Test()]
        public void CalculateCostTest_ShouldBeEqual_WhenGivenCorrectValues(
            DateTime checkIn,
            DateTime checkOut, int expected)

        {
            //Acording to all documentation, this should work but dosen´t..haha
            //string format = "yyyy-MM-dd HH:mm";
            CultureInfo cultureinfo = new CultureInfo("sv-SE");
            //var test = DateTime.ParseExact(checkIn.ToString(), format, cultureinfo);
            //var test1 = DateTime.ParseExact(checkOut.ToString(), format, cultureinfo);

            var cI = checkIn.ToString("yyyy-MM-dd hh:mm");
            var cO = checkOut.ToString("yyyy-MM-dd hh:mm");

            DateTime dT1 = DateTime.Parse(cI, cultureinfo);
            DateTime dT2 = DateTime.Parse(cO, cultureinfo);

            var actual = pl.CalculateCost(dT1, dT2);
            var test = checkIn.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}

////DateTime.Parse(cI, cultureinfo, DateTimeStyles.AdjustToUniversal),
//DateTime.Parse(cO, cultureinfo, DateTimeStyles.AdjustToUniversal
//)