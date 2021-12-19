namespace Parkeringshuset.Helpers
{
    using Parkeringshuset.Controllers;
    using Parkeringshuset.Data;
    using Parkeringshuset.Models;
    using System;

    public static class SeedDataHelper
    {
        public static ParkeringGarageContext Db = new();
           
        /// <summary>
        /// Add mock data into database.
        /// </summary>
        public static void RunMock()
        {
            try
            {
                Db.Ptypes.Add(new PType { Name = "Handicap", TotalSpots = 10 });
                Db.Ptypes.Add(new PType { Name = "Regular", TotalSpots = 120 });
                Db.Ptypes.Add(new PType { Name = "Electric", TotalSpots = 30 });
                Db.Ptypes.Add(new PType { Name = "Monthly", TotalSpots = 30 });
                Db.Ptypes.Add(new PType { Name = "Motorbike", TotalSpots = 10 });

                Db.SaveChanges();
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// Creates an admin.
        /// </summary>
        public static void CreateAdmin()
        {
            AdminController ac = new();
            ac.Create("admin", "admin123", "Parking.System.Owner@gmail.com");
        }
    }
}