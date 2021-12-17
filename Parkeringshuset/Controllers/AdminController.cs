namespace Parkeringshuset.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using Parkeringshuset.Data;
    using Parkeringshuset.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

   public class AdminController
    {
        public ParkeringGarageContext db = new();
        Admin Admin;


        #region Crud
        /// <summary>
        /// Get Admin object by sending in username and password in strings. 
        /// </summary>
        /// <param name="username">For admin.</param>
        /// <param name="password">For admin.</param>
        /// <returns>Admin object or null.</returns>
        public Admin GetAdmin(string username, string password)
        {
            return db.Admins.FirstOrDefault(x => x.Username == username && x.Password == password) ?? null;

        }

    /// <summary>
    /// Creates an Admin in database if username isn´t taken. 
    /// </summary>
    /// <param name="username">string not hashed.</param>
    /// <param name="password">string, not hashed.</param>
    /// <param name="email"></param>
    /// <returns>True if admin object is created and false if username already exists.</returns>
        public bool Create(string username, string password, string email)
        {
            var isUserNameAlreadyUsed = db.Admins.FirstOrDefault(x => x.Username == username);

            if (isUserNameAlreadyUsed is null)
            {
                LoginController lc = new();
                var salt = lc.GenerateSalt(128);
                var hash = lc.GenerateSha256(password, salt);
                Admin admin = new();
                admin.Username = username;
                admin.Password = hash;
                admin.Email = email;
                admin.Salt = salt;
                db.Admins.Add(admin);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Delete an admin in database. 
        /// </summary>
        /// <param name="admin">object of admin. Needs ID(PK in DB) to be able to indetify.</param>
        /// <returns>True if object is deleted. False if admin coulnd´t be found in DB.</returns>
        public bool Delete(Admin admin)
        {
            if (admin is not null)
            {
                Admin = db.Admins.FirstOrDefault(x => x.Id == admin.Id);

                if (Admin is not null)
                {
                    db.Admins.Remove(Admin);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Update Admin object in database.
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>true if successfull, false if database issues.</returns>
        public bool Update(Admin admin)
        {
            try
            {
                db.Update(Admin);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false; 
            }
    
        }

        #endregion

        /// <summary>
        /// Get the cars that been parked in the garage during specific dates. 
        /// </summary>
        /// <param name="from">Start date for your search.</param>
        /// <param name="to">End date for your search.</param>
        /// <returns>A list with Parking Tickets from those dates. Car that is still parked are 
        /// included. The List can be null</returns>
        public List<PTicket> GetTicketForDate(DateTime from, DateTime to)
        {
            // RIGHT: this will include the last day
            var toDateExclusive = to.AddDays(1);

            return db.Ptickets.Where(
                t => t.CheckedInTime.Date >= from.Date && t.CheckedInTime <= toDateExclusive.Date 
                ||
                t.CheckedOutTime >= from.Date && t.CheckedOutTime <= toDateExclusive.Date)
                .ToList();        
        }
        /// <summary>
        /// Get all tickets in Database. 
        /// </summary>
        /// <returns>A list of tickets. The List can be null.</returns>
        public List<PTicket> GetAllTickets()
        {
            return db.Ptickets.Select(x=>x).ToList();
            
        }

        /// <summary>
        /// Get all tickets that are registred as monthly tickets. 
        /// </summary>
        /// <returns></returns>
        public List<PTicket> GetActiveMonthlyTickets()
        {
           

            return db.Ptickets.Include(x => x.Type).Where(x => x.Type.Name == ParkingTypesNames
            .Monthly).ToList();
  
        }
        public List<PType> GetAllParkingTypes()
        {
            return db.Ptypes.Select(x => x).ToList();
        }
    }
}
