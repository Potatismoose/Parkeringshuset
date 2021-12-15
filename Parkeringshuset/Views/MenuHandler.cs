namespace Parkeringshuset.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.ConsoleKey;

    public static class MenuHandler
    {
        private static string regNr = "";
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
        /// Reads input key by key and determines wether to open secret admin login view, or
        /// register a registration number for use in MainMenu().
        /// </summary>
        public static void UserInput()
        {
            ConsoleKeyInfo pressedKey;
            do
            {
                pressedKey = Console.ReadKey();
                if (pressedKey.Key == Enter)
                {
                    SetRegNr();
                    break;
                }

                if (pressedKey.Key != RightArrow &&
                    pressedKey.Key != LeftArrow &&
                    pressedKey.Key != UpArrow &&
                    pressedKey.Key != DownArrow
                )
                {
                    secretPatternMatch.Clear();

                    regNr += (char)pressedKey.Key;
                }
                else
                {
                    secretPatternMatch.Add(pressedKey.Key);
                    if (IsPatternMatch())
                    {
                        AdminLoginUnlocked();
                        break;
                    }
                }
            } while (pressedKey.Key != Enter || !IsPatternMatch());
        }

        /// <summary>
        /// Sets registration number if valid.
        /// </summary>
        private static void SetRegNr()
        {
            if (IsRegNrValid())
            {
                MainMenu.regNr = regNr.Replace("\r", string.Empty);
                regNr = string.Empty;
            }
        }

        /// <summary>
        /// Determines if regNr is in range. Displays error message if not.
        /// </summary>
        /// <returns>True if RegNr is valid, false if not.</returns>
        private static bool IsRegNrValid()
        {
            bool isRegNrValid = true;
            if (regNr.Length > 7)
            {
                Helper.DisplayHelper.DisplayRed("Registration number must be 7 characters or " +
                    "less!");
                MainMenu.PressAnyKeyToContinue();
                isRegNrValid = false;
                regNr = "";
            }
            else if (regNr.Length < 2)
            {
                Helper.DisplayHelper.DisplayRed("Registration number must be 2 characters or " +
                    "more!");
                MainMenu.PressAnyKeyToContinue();
                isRegNrValid = false;
                regNr = "";
            }
            return isRegNrValid;
        }

        /// <summary>
        /// Checks if the secret admin menu can be accessed.
        /// </summary>
        /// <returns>Returns true if secretPatternMatch matches secretPattern.</returns>
        private static bool IsPatternMatch()
        {
            return secretPatternMatch.SequenceEqual(secretPattern);
        }

        /// <summary>
        /// Creates admin menu object and calls the PrintAdminPage().
        /// </summary>
        private static void AdminLoginUnlocked()
        {
            secretPatternMatch.Clear();
            AdminMenu login = new();
            var admin = login.LoginAdmin();
            if (admin is not null)
            {
                login.PrintAdminPage(admin);
            }
            else
            {
                Helper.DisplayHelper.DisplayRed("Something went wrong!");
                MainMenu.PressAnyKeyToContinue();
            }
        }
    }
}