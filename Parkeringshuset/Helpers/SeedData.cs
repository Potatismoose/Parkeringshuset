namespace Parkeringshuset.Helpers
{
    using Parkeringshuset.Controllers;
    using Parkeringshuset.Data;
    using Parkeringshuset.Helper;
    using Parkeringshuset.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class SeedData
    {
        public static ParkeringGarageContext Db = new();

        public static void RunMock()
        {
            try
            {
                Db.Ptypes.Add(new PType { Name = "Handicap", TotalSpots = 10 });
                Db.Ptypes.Add(new PType { Name = "Regular", TotalSpots = 120 });
                Db.Ptypes.Add(new PType { Name = "Electric", TotalSpots = 30 });
                Db.Ptypes.Add(new PType { Name = "Monthly", TotalSpots = 30 });
                Db.Ptypes.Add(new PType { Name = "Motorbike", TotalSpots = 10 });
                Db.Admins.Add(new Admin { Username = "admin", Password = "admin123", Email = "parking.garage.boss@gmail.com" });

                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateAdmin()
        {
            AdminController ac = new();
            ac.Create("admin", "admin123", "parking.garage.boss@gmail.com");
        }
    }
}