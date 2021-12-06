using System.Collections.Generic;
using System;
using Parkeringshuset.Models;

namespace Parkeringshuset.Views
{
    public static class MainMenu
    {

        public static void RunMainMenu()
        {
            Console.WriteLine("Enter your registration number: ");
            string regNr = Console.ReadLine();
            if (string.IsNullOrEmpty(regNr.Trim()))
            {
                Console.WriteLine("Can not use empty registration number, please try again.");
            }
            if (ParkingMeterController.GetActiveTicket(regNr) == null)
            {
                ParkingTicketController.CheckIn(regNr);
                Console.WriteLine($"Checked In, what zone would you like to park in?: ");
                Console.WriteLine("1. Normal vehicle");
                Console.WriteLine("2. Electric vehicle");
                Console.WriteLine("3. Handicaped");
                Console.WriteLine("4. Monthly");
                Console.WriteLine("5. Motorcycle");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine();
                    default:
                        break;
                }
            }
            else if (ParkingMeterController.GetActiveTicket(regNr).Type.Name == "Monthly")
            {
                Console.WriteLine("Welcome back!");
            }
            else
            {
                ParkingTicketController.Checkout(regNr);
            }


        }
    }
}
