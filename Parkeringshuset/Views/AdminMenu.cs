using Parkeringshuset.Models;
using System;
using System.Collections.Generic;

namespace Parkeringshuset.Views
{
    public class Login
    {
        private List<string> LoginOptions = new() { "Username", "Password" };
        private const string AdminUser = "Admin";
        private const string AdminPassword = "Controll";

        public bool PrintLoginPage()
        {
            //ParkingGarage pg = new(12);
            //Vehicle car = new("NPK144");
            //Vehicle car2 = new("OSS432");
            //Console.WriteLine($"Lediga platser: {pg.CalculateAvailibleSpace()}");
            //Console.WriteLine("\tCheckar in två bilar...");
            //Console.WriteLine($"Is car parked already? {pg.IsCarParkedInGarageAlready(car)}");
            //pg.ParkingMeters[0].CreateTicket(car);
            //pg.ParkingMeters[0].CreateTicket(car2);

            //Vehicle car3 = new("NPK144");
            //Console.WriteLine($"Is car parked already? {pg.IsCarParkedInGarageAlready(car3)}");
            //Console.WriteLine($"\t{car}\n\t{car2}");
            //Console.WriteLine($"Lediga platser: {pg.CalculateAvailibleSpace()}");
            //Console.WriteLine("\tCheckar ut en av bilarna");
            //Console.WriteLine($"\tSuccessful? {pg.ParkingMeters[0].CheckoutVehicle(new Vehicle("OSS432"))}");
            //Console.WriteLine($"\t{car2}");
            //Console.WriteLine($"Lediga platser: {pg.CalculateAvailibleSpace()}");
            //Console.ReadKey();

            //Console.WriteLine("Parkeringsgaraget. Logga in som admin eller lämna blankt för att starta parkeringsappen.");
            //Console.ReadKey();

            //foreach (var parkingmeter in pg.ParkingMeters)
            //{
            //    foreach (var ticket in parkingmeter.GetAllSoldTickets(new DateTime(2021, 09, 14), new DateTime(2021, 09, 16)))
            //    {
            //        Console.WriteLine(ticket.ToString());
            //        Console.WriteLine(ticket.CalculateCost());

            //    }
            //}

            //Console.ReadKey();
            return false;
        }

        private static bool LoginAdmin(string username, string password)
        {
            return (
                username == AdminUser
                && password == AdminPassword
                ? true : false);
        }
    }
}