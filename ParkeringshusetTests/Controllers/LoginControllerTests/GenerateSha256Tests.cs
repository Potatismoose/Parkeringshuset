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
            string salt = "W+ynecj6X7TaW+jVo19cGw==";
            string s = _lc.GenerateSalt(128);

            Assert.AreEqual("faaf82c81a19035218eabcf879c9c07073cc2db0cde7b3a624a0a2e5a18ebb94",
                _lc.GenerateSha256(password, salt));
        }
    }
}