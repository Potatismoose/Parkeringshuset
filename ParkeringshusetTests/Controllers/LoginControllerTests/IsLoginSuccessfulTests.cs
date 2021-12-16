using NUnit.Framework;
using Parkeringshuset.Controllers;
using Parkeringshuset.Data;

namespace ParkeringshusetTests.Controllers.LoginControllerTests
{
    [Category("UnitTests")]
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
            //Arrange
            string username = "user123";
            string password = "qwerty123";
            //Act
            var result = _lc.LoginReturnAdmin(username, password);
            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void IsLoginSuccessful_CorrectUsernameWrongPassword_ReturnFalse(){
            //Arrange
            string username = "admin";
            string password = "qwerty123";
            //Act
            var result = _lc.LoginReturnAdmin(username, password);
            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void IsLoginSuccessful_WrongUsernameCorrectPassword_ReturnFalse(){
            //Arrange
            string username = "user123";
            string password = "admin123";
            //Act
            var result = _lc.LoginReturnAdmin(username, password);
            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void IsLoginSuccessful_CorrectUsernameAndPassword_ReturnTrue(){
            //Arrange
            string username = "admin";
            string password = "admin123";
            //Act
            var result = _lc.LoginReturnAdmin(username, password);
            //Assert
            Assert.That(result, Is.Not.Null);
        }
    }
}