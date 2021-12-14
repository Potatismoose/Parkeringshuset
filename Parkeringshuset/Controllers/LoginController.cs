using System.Linq;
using Parkeringshuset.Data;
using Parkeringshuset.Models;

namespace Parkeringshuset.Controllers
{
    public class LoginController
    {
        private ParkeringGarageContext db = new();

        /// <summary>
        /// Checks if the login was successful.
        /// </summary>
        /// <param name="username">The username for the user.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>True if the login was successful, otherwise false.</returns>
        public Admin LoginReturnAdmin(string username, string password){
            var admin = db.Admins.FirstOrDefault(x => x.Username == username && x.Password == password);

            return admin;
        }
    }
}