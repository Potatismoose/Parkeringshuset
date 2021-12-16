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
        public Admin LoginReturnAdmin(string username, string password){
            var admin = db.Admins.FirstOrDefault(x => x.Username == username && 
            x.Password == password);

            return admin;
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

        public string GenerateSha256(string password, byte[] salt){
            byte[] pass = Encoding.ASCII.GetBytes(password);
            List<byte> tmp = new();
            tmp.AddRange(pass);
            tmp.AddRange(salt);
            byte[] hashedPassword = new byte[tmp.Count];
            var sha256 = SHA256.Create();
            sha256.ComputeHash(hashedPassword);
            return Convert.ToBase64String(sha256.Hash) + Convert.ToBase64String(salt);
        }
    }
}