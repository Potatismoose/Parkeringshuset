using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Parkeringshuset.Data;

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
        public bool IsLoginSuccessful(string username, string password){
            var admin = db.Admins.FirstOrDefault(x => x.Username == username && x.Password == password);

            return admin != null;
        }

        /// <summary>
        /// Generates a random number of nonzero values.
        /// </summary>
        /// <param name="size">The size of the salt.</param>
        /// <returns>A byte[] with the salt.</returns>
        public byte[] GenerateSalt(int size){
            byte[] salt = new byte[size];
            var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(salt);

            return salt;
        }

        public byte[] GenerateSha256(string password){
            byte[] pass = Encoding.ASCII.GetBytes(password);
            List<byte> tmp = new();
            tmp.AddRange(pass);
            tmp.AddRange(GenerateSalt(128));
            byte[] hashedPassword = new byte[tmp.Count];

            using (var sha256 = SHA256.Create()){
                sha256.ComputeHash(hashedPassword);
            }

            return hashedPassword;
        }
    }
}