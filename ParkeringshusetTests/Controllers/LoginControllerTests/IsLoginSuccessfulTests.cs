using NUnit.Framework;
using Parkeringshuset.Controllers;
using Parkeringshuset.Data;

namespace ParkeringshusetTests.Controllers.LoginControllerTests
{
    [TestFixture]
    public class IsLoginSuccessfulTests
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
        public void IsLoginSuccessful_WrongUsernameAndPassword_ReturnFalse(){
            string username = "user123";
            string password = "qwerty123";

            Assert.IsFalse(_lc.IsLoginSuccessful(username, password));
        }

        [Test]
        public void IsLoginSuccessful_CorrectUsernameWrongPassword_ReturnFalse(){
            string username = "admin";
            string password = "qwerty123";

            Assert.IsFalse(_lc.IsLoginSuccessful(username, password));
        }

        [Test]
        public void IsLoginSuccessful_WrongUsernameCorrectPassword_ReturnFalse(){
            string username = "user123";
            string password = "admin123";

            Assert.IsFalse(_lc.IsLoginSuccessful(username, password));
        }

        [Test]
        public void IsLoginSuccessful_CorrectUsernameAndPassword_ReturnTrue(){
            string username = "admin";
            string password = "admin123";

            Assert.IsTrue(_lc.IsLoginSuccessful(username, password));
        }
    }
}