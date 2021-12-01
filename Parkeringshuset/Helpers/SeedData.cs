namespace Parkeringshuset.Helpers
{
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
        public static void Mock()
        {

        //    if (Db.pSpots.Any())
        //    {
               
        //    }
        //    else
        //    {
        //        // Initial seed to create how many parking spots that exsits in the garage. 
        //        PSpot pSpot = new();
        //        pSpot.Monthly = 30;
        //        pSpot.Handicap = 10;
        //        pSpot.Regular = 120;
        //        pSpot.Motorbike = 10;
        //        pSpot.Electric = 30;


        //        Db.pSpots.Add(pSpot);
        //        Db.SaveChanges();
        //    }

        //    var firstID = Db.pSpots.FirstOrDefault(p => p.Id == 1);
        //    Console.WriteLine(firstID.Monthly); 


        }

        public static void TestPType()
        {
            var hcp = Db.Ptypes.FirstOrDefault(p => p.Name == "Handicap");
            DisplayHelper.DisplayGreen(hcp.TotalSpots.ToString());

            try
            {
                Db.Ptypes.Add(new PType { Name = "Handicap", TotalSpots = 10 });
                Db.Ptypes.Add(new PType { Name = "Regular", TotalSpots = 120 });
                Db.Ptypes.Add(new PType { Name = "Electric", TotalSpots= 30 });
                Db.Ptypes.Add(new PType { Name = "Monthly", TotalSpots= 30 });
                Db.Ptypes.Add(new PType { Name = "Motorbike", TotalSpots=10 });

                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


           
        }
    }
}
