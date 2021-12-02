using NUnit.Framework;
using Parkeringshuset.BusinessLogic;

namespace Parkeringshuset.BusinessLogic.Tests
{
    [TestFixture()]
    public class ParkingMeterLogicTests
    {
        [Test()]
        public void CalculateCost()
        {
            ParkingMeterLogic pmLogic = new();
            var result = pmLogic.CalculateCost();
           
            Assert.AreEqual(result, 110);
        }
    }
}