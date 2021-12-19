using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        public Admin LoginReturnAdmin(string username, string password)
        {
            var admin = db.Admins.FirstOrDefault(x => x.Username == username);
            if(admin == null)
            {
                return null;
            }
            var salt = admin.Salt;
            string pass = GenerateSha256(password, salt);

            if (admin.Password != pass)
            {
                return null;
            }

            return admin;
        }

        /// <summary>
        /// Generates a random number of nonzero values.
        /// </summary>
        /// <param name="size">The size of the salt.</param>
        /// <returns>A byte[] with the salt.</returns>
        public string GenerateSalt(int size)
        {
            byte[] salt = new byte[size];
            var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Generates a SHA256 hash with the password and the salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>The generated hash.</returns>
        public string GenerateSha256(string password, string salt)
        {
            string passwordAndHash = string.Concat(password, salt);
            var sha256 = SHA256.Create();
            byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordAndHash));

            StringBuilder sb = new();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}