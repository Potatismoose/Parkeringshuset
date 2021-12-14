using System.Collections.Generic;
using System;
using Parkeringshuset.Models;
using Parkeringshuset.BusinessLogic;
using System.Linq;
using static System.ConsoleKey;
using System.Text.RegularExpressions;
using Parkeringshuset.Helpers;

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

        /// <summary>
        /// Check in and out menu for users of the parking garage.
        /// </summary>
        public static void RunMainMenu()
        {
            bool keepGoing = true;
            bool isValidRegNr = false;
            bool isCharOrDigit = false;
            bool hasAdminLoggedOut = false;
            string pType = "";
            string regNr = "";
            string confirmedRegNr = "";

            ParkingMeterLogic pML = new();
            ParkingTicketController pTC = new();
            do
            {
                isValidRegNr = false;
                hasAdminLoggedOut = false;
                Console.Clear();
                Console.Write("Enter your registration number: ");
                do
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey();
                    secretPatternMatch.Add(pressedKey.Key);

                    if (pressedKey.Key != RightArrow &&
                        pressedKey.Key != LeftArrow &&
                        pressedKey.Key != UpArrow &&
                        pressedKey.Key != DownArrow
                    )
                    {
                        isCharOrDigit = true;
                        regNr += (char)pressedKey.Key;
                        if (regNr.Length >= 2 && regNr.Length <= 7 && pressedKey.Key == Enter)
                        {
                            isValidRegNr = true;
                            confirmedRegNr = regNr;
                            regNr = "";
                            break;
                        }
                    }
                    else
                    {
                        isCharOrDigit = false;
                    }

                    if (isCharOrDigit)
                    {
                        secretPatternMatch.Clear();
                    }
                    if (secretPatternMatch.SequenceEqual(secretPattern))
                    {
                        secretPatternMatch.Clear();
                        AdminMenu login = new();
                        var admin = login.LoginAdmin();
                        if (admin is not null)
                        {
                            login.PrintAdminPage(admin);
                            hasAdminLoggedOut = true;
                        }
                        else
                        {
                            hasAdminLoggedOut = true;
                            Helper.DisplayHelper.DisplayRed("Something went wrong!");
                            PressAnyKeyToContinue();
                        }
                    }
                    if (hasAdminLoggedOut)
                    {
                        break;
                    }
                } while (!isValidRegNr || !secretPatternMatch.SequenceEqual(secretPattern));

                if (hasAdminLoggedOut)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(confirmedRegNr.Trim()))
                {
                    Console.WriteLine("Can not use empty registration number, please try again.");
                    PressAnyKeyToContinue();
                    continue;
                }
                var parkingTicket = pTC.GetActiveTicket(confirmedRegNr);

                if (parkingTicket is null)
                {
                    Console.WriteLine(confirmedRegNr);
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
                            pML.CheckIn(confirmedRegNr, pType);
                            PressAnyKeyToContinue();
                            break;

                        case 2:
                            pType = "Electric";
                            pML.CheckIn(confirmedRegNr, pType);
                            PressAnyKeyToContinue();
                            break;

                        case 3:
                            pType = "Handicap";
                            pML.CheckIn(confirmedRegNr, pType);
                            PressAnyKeyToContinue();
                            break;
                        //case 4:
                        //    pType = "Monthly";
                        //    pML.CheckIn(regNr, pType);
                        //    PressAnyKeyToContinue();
                        //    break;
                        case 5:
                            pType = "Motorbike";
                            pML.CheckIn(confirmedRegNr, pType);
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
                    secretPatternMatch.Clear();
                }
                else if (pTC.IsMonthly(parkingTicket) && pTC.IsTicketActive(parkingTicket))
                {
                    Console.WriteLine($"Welcome back! Your ticket expires { parkingTicket.CheckedOutTime}");
                    secretPatternMatch.Clear();
                    PressAnyKeyToContinue();
                }
                else if (pTC.IsTicketActive(parkingTicket))
                {
                    pTC.CheckOut(parkingTicket);
                    Console.WriteLine("Checked out. Thank you for using our garage, welcome back!");
                    secretPatternMatch.Clear();
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