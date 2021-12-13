using NUnit.Framework;
using Parkeringshuset.Controllers;

namespace ParkeringshusetTests.Controllers.LoginControllerTests
{
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
            var salt = _lc.GenerateSalt(16);
            
            Assert.AreEqual(_lc.GenerateSha256(password), salt);
        }
    }
}