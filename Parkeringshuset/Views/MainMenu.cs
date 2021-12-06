using System.Collections.Generic;
using System;
using Parkeringshuset.Models;

namespace Parkeringshuset.Views
{
    public static class MainMenu
    {
        public static void RunMainMenu()
        {
            bool keepGoing = true;
            do
            {
                Console.Write("Enter your registration number: ");
                string regNr = Console.ReadLine();
                if (string.IsNullOrEmpty(regNr.Trim()))
                {
                    Console.WriteLine("Can not use empty registration number, please try again.");
                    continue;
                }
                var parkingTicket = ParkingMeterController.GetActiveTicket(regNr);
                if (parkingTicket is null)
                {
                    ParkingTicketController.CheckIn(regNr);
                    Console.WriteLine($"Checked In, what zone would you like to park in?: ");
                    Console.WriteLine("1. Normal vehicle");
                    Console.WriteLine("2. Electric vehicle");
                    Console.WriteLine("3. Handicaped");
                    Console.WriteLine("4. Monthly");
                    Console.WriteLine("5. Motorcycle");
                    Console.WriteLine("6. Abort check in");
                    int.TryParse(Console.ReadLine(), out int choice);
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine();
                            break;
                        case 2:
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.WriteLine();
                            break;
                        case 4:
                            Console.WriteLine();
                            break;
                        case 5:
                            Console.WriteLine();
                            break;

                        default:
                            break;
                    }
                }
                else if (parkingTicket.Type.Name == "Monthly")
                {
                    Console.WriteLine("Welcome back!");
                    keepGoing = false;
                }
                else
                {
                    ParkingTicketController.CheckOut(parkingTicket);
                    Console.WriteLine("Thank you for using our garage, welcome back");
                    keepGoing = false;
                }
            } while (keepGoing);


        }
    }
}
