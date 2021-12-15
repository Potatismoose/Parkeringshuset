using NUnit.Framework;
using Parkeringshuset.Controllers;

namespace ParkeringshusetTests.Controllers.LoginControllerTests
{
    [Category("UnitTests")]
    [TestFixture]
    public class GenerateSha256Tests
    {
        private LoginController _lc;

        [SetUp]
        public void SetUp(){
            this._lc = new();
        }

        [TearDown]
        public void CleanUp(){
            _lc = null;
        }

        [Test]
        public void GenerateSha256_GeneratesACorrectHash_ReturnTrue(){
            string password = "qwerty123";
            byte[] salt = new byte[] {
                91, 236, 167, 121, 200, 250, 95, 180, 218, 91, 232, 213, 163, 95, 92, 27};

            Assert.AreEqual(_lc.GenerateSha256(password, salt), 
                "YRJt4beVuXbzrIePSOiPp3qH1zCLpXx2QrnhBoQDpJY=W+ynecj6X7TaW+jVo19cGw==");
        }
    }
}