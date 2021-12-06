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
            var result = CalculateCostLogic.Cost();
           
            Assert.AreEqual(270,result);
        }
    }
}