namespace Parkeringshuset.Data
{
    using Microsoft.EntityFrameworkCore;
    using Parkeringshuset.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ParkeringGarageContext : DbContext
    {
        public string DatabaseName = "ParkingGarage.db";
        public DbSet<PTicket> Ptickets { get;set; }

        public DbSet<PType> Ptypes { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Admin> Admins { get; set; }


        


        /// <summary>
        /// Configs to database in local folder. 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var myfolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent.Parent.Parent.ToString();
            var path = Path.Combine(myfolder, "Databases");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, DatabaseName);
            optionsBuilder.UseSqlite($"Data Source ={path}");
        }
    }
}
