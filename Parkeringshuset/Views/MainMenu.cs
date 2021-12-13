using System.Collections.Generic;
using System;
using Parkeringshuset.Models;
using Parkeringshuset.BusinessLogic;
using System.Linq;
using static System.ConsoleKey;
using System.Text.RegularExpressions;

namespace Parkeringshuset.Views
{
    public static class MainMenu
    {
        private static List<ConsoleKey> secretPatternMatch = new List<ConsoleKey>();

        private static List<ConsoleKey> secretPattern = new List<ConsoleKey>(){
                    RightArrow,
                    LeftArrow,
                    RightArrow,
                    LeftArrow,
                    UpArrow,
                    LeftArrow,
                    RightArrow};

        private static string regNr = "";

        /// <summary>
        /// Check in and out menu for users of the parking garage.
        /// </summary>
        public static void RunMainMenu()
        {
            bool keepGoing = true;
            bool isValidRegNr = false;
            bool isCharOrDigit = false;
            bool isLoginSuccessful = false;
            bool haveAdminBeenLoggedIn = false;
            string pType = "";

            ParkingMeterLogic pML = new();
            ParkingTicketController pTC = new();
            do
            {
                Console.Clear();
                Console.Write("Enter your registration number: ");
                do
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey();
                    secretPatternMatch.Add(pressedKey.Key);

                    foreach (var item in secretPatternMatch)
                    {
                        if (item != RightArrow &&
                            item != LeftArrow &&
                            item != UpArrow &&
                            item != DownArrow
                        )
                        {
                            isCharOrDigit = true;
                            regNr += (char)pressedKey.Key;
                            if (regNr.Length >= 2 && regNr.Length <= 7 && pressedKey.Key == Enter)
                            {
                                isValidRegNr = true;
                            }
                        }
                        else
                        {
                            isCharOrDigit = false;
                        }
                    }
                    if (isCharOrDigit)
                    {
                        secretPatternMatch.Clear();
                    }
                    else if (secretPatternMatch.SequenceEqual(secretPattern))
                    {
                        AdminMenu login = new();
                        if (isLoginSuccessful == login.LoginAdmin())
                        {
                            login.PrintAdminPage();
                            haveAdminBeenLoggedIn = true;
                        }
                        else
                        {
                            Helper.DisplayHelper.DisplayRed("Something went wrong!");
                        }
                    }
                } while (!isValidRegNr && !secretPatternMatch.SequenceEqual(secretPattern));

                if (haveAdminBeenLoggedIn)
                {
                    haveAdminBeenLoggedIn = false;
                    continue;
                }

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

        private static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }
    }
}