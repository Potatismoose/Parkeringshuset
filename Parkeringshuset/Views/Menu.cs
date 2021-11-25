using Parkeringshuset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshuset.Views
{
    internal class Menu
    {
        List<string> MenuOptions = new() { "Check in vehicle", "Check out vehicle" };
        public ParkingGarage Pg { get; }
    
        public Menu(ParkingGarage pg)
        {
            Pg = pg;
        }

        public void PrintMainMenu()
        {
            bool endMenu = false;
            do
            {
                Console.Clear();
                for (int i = 0; i < MenuOptions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {MenuOptions[i]}");
                }

                Console.WriteLine();
                Console.Write("Choose option: ");
                var userInput = Console.ReadLine();
                var successfullyConverted = int.TryParse(userInput, out int inputNumber);
                if (successfullyConverted)
                {
                    SendUserToOption(inputNumber);

                }
                else {
                    endMenu = SendUserToOption(userInput);
                }
            } while (!endMenu);
        }

        private void SendUserToOption(int userOption)
        {
            switch (userOption)
            {
                case 1:
                    CheckInVehicle();
                    break;
                case 2:
                    CheckoutVehicle();
                    break;
                default:
                    break;
            }
        }

        private bool SendUserToOption(string userOption)
        {
            switch (userOption)
            {
                case "CloseApp":
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }

        private void CheckInVehicle()
        {
            bool noValidRegNr = true;

            if(Pg.CalculateAvailibleSpace() > 0)
            {     
                while (noValidRegNr)
                {                
                    Console.WriteLine("Please type in you registration number");
                    string registrationsNr = Console.ReadLine();

                    if (registrationsNr.Length > 5 && registrationsNr.Length < 7)
                    {
                        var vehicle = new Vehicle(registrationsNr);

                        if (!Pg.IsCarParkedInGarageAlready(vehicle))
                        {                          
                            Console.WriteLine(Pg.ParkingMeters[0].CreateTicket(vehicle));
                            noValidRegNr = false;
                        }
                        else
                        {
                            Console.WriteLine("Man (or women), you must be stupid. You have already a ticket.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please type in a valid Registration Number");
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no empty space in the parking garage, get the fuck out!");
            }
        }

        private void CheckoutVehicle()
        {
            var carIsNotCheckedOut = true;
            while (carIsNotCheckedOut)
            {
                Console.WriteLine("Please type in your registration number of the car you want to check out");
                var batMobil = new Vehicle(Console.ReadLine());
                 var ticket = Pg.ParkingMeters[0].CheckoutVehicle(batMobil);

                if (ticket.checkOutSuccessful)
                {
                    Console.WriteLine("Your ticket is now cancelled");
                    Console.WriteLine($"your final cost is {ticket.ticket.CalculateCost()} kr");
                }

                else
                {
                    Console.WriteLine("Your car doesn´t have a valid ticket. Did you type in wrong reg number?");
                }
            }
            
        }
    }
}