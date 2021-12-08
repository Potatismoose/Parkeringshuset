using System.Collections.Generic;
using System;
using Parkeringshuset.Models;
using Parkeringshuset.BusinessLogic;
using System.Linq;

namespace Parkeringshuset.Views
{
    public static class MainMenu
    {
        /// <summary>
        /// Check in and out menu for users of the parking garage.
        /// </summary>
        public static void RunMainMenu()
        {
            bool keepGoing = true;
            string pType = "";
            char firstChar = '\0';

            ParkingMeterLogic pML = new();
            ParkingTicketController pTC = new();
            do
            {
                Console.Clear();
                Console.Write("Enter your registration number: ");
                UnlockAdminMenu(firstChar);

                string regNr = Console.ReadLine().ToUpper();
                regNr = firstChar + regNr;

                if (string.IsNullOrEmpty(regNr.Trim()))
                {
                    Console.WriteLine("Can not use empty registration number, please try again.");
                    PressAnyKeyToContinue();
                    continue;
                }
                var parkingTicket = pTC.GetActiveTicket(regNr);

                if (parkingTicket is null)
                {
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
                    Console.WriteLine($"Welcome back! Your ticket expires { parkingTicket.CheckedOutTime}");
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

        private static char UnlockAdminMenu(char firstChar)
        {
            List<ConsoleKey> secretPatternMatch = new List<ConsoleKey>();
            List<ConsoleKey> secretPattern = new List<ConsoleKey>() {
                    ConsoleKey.RightArrow,
                    ConsoleKey.LeftArrow,
                    ConsoleKey.RightArrow,
                    ConsoleKey.LeftArrow,
                    ConsoleKey.UpArrow,
                    ConsoleKey.LeftArrow,
                    ConsoleKey.RightArrow};

            ConsoleKeyInfo pressedKey = Console.ReadKey();

            if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                secretPatternMatch.Add(pressedKey.Key);
                secretPatternMatch.Add(Console.ReadKey().Key);
                while (pressedKey.Key != ConsoleKey.Enter)
                {
                    pressedKey = Console.ReadKey();
                    secretPatternMatch.Add(pressedKey.Key);

                    if (secretPatternMatch.SequenceEqual(secretPattern))
                    {
                        Console.WriteLine("You unlocked it!");
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                firstChar = (char)pressedKey.Key;
            }

            return firstChar;
        }

        private static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }
    }
}