using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Models;
using System;

namespace Parkeringshuset.Views
{
    public static class MainMenu
    {
        public static string regNr;

        /// <summary>
        /// Check in and out menu for users of the parking garage.
        /// </summary>
        public static void RunMainMenu()
        {
            bool keepGoing = true;

            string pType = "";

            ParkingMeterLogic pML = new();
            ParkingTicketController pTC = new();
            do
            {
                regNr = "";
                Console.Clear();
                Console.Write("Enter your registration number: ");

                MenuHandler.UserInput();

                if (string.IsNullOrEmpty(regNr))
                {
                    continue;
                }

                var parkingTicket = pTC.GetActiveTicket(regNr);

                if (parkingTicket is null)
                {
                    Console.WriteLine(regNr);
                    Console.WriteLine($"Checked In, what zone would you like to park in?: ");
                    Console.WriteLine("1. Regular vehicle");
                    Console.WriteLine("2. Electric vehicle");
                    Console.WriteLine("3. Handicaped");
                    //Console.WriteLine("4. Monthly");
                    Console.WriteLine("5. Motorbike");
                    Console.WriteLine("6. Abort check in");
                    int.TryParse(Console.ReadLine(), out int choice);
                    switch (choice)
                    {
                        case 1:
                            pType = "Regular";
                            pML.CheckIn(regNr, pType);
                            PressAnyKeyToContinue();
                            break;

                        case 2:
                            pType = "Electric";
                            pML.CheckIn(regNr, pType);
                            PressAnyKeyToContinue();
                            break;

                        case 3:
                            pType = "Handicap";
                            pML.CheckIn(regNr, pType);
                            PressAnyKeyToContinue();
                            break;
                        //case 4:
                        //    pType = "Monthly";
                        //    pML.CheckIn(regNr, pType);
                        //    PressAnyKeyToContinue();
                        //    break;
                        case 5:
                            pType = "Motorbike";
                            pML.CheckIn(regNr, pType);
                            PressAnyKeyToContinue();
                            break;

                        case 6:
                            keepGoing = false;
                            break;

                        default:
                            Console.WriteLine("Jerry created a problem, please try again!");
                            PressAnyKeyToContinue();
                            break;
                    }
                }
                else if (pTC.IsMonthly(parkingTicket) && pTC.IsTicketActive(parkingTicket))
                {
                    Console.WriteLine($"Welcome back! Your ticket expires " +
                        $"{ parkingTicket.CheckedOutTime}");
                    PressAnyKeyToContinue();
                }
                else if (pTC.IsTicketActive(parkingTicket))
                {
                    pTC.CheckOut(parkingTicket);
                    Console.WriteLine("Checked out. Thank you for using our garage, welcome back!");
                    PressAnyKeyToContinue();
                }
            } while (keepGoing);
        }

        /// <summary>
        /// Gives user opportunity to read message and change between two views.
        /// </summary>
        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }
    }
}