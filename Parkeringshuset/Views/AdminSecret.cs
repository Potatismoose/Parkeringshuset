namespace Parkeringshuset.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.ConsoleKey;

    public static class AdminSecret
    {
        public static List<ConsoleKey> secretPatternMatch = new List<ConsoleKey>();

        public static List<ConsoleKey> secretPattern = new List<ConsoleKey>(){
                    RightArrow,
                    LeftArrow,
                    RightArrow,
                    LeftArrow,
                    UpArrow,
                    LeftArrow,
                    RightArrow};

        public static bool hasAdminLoggedOut = false;
        public static bool isValidRegNr = false;
        private static string confirmedRegNr = "";

        public static void UserInput()
        {
            while (!isValidRegNr || !secretPatternMatch.SequenceEqual(secretPattern))
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                if (confirmedRegNr.Length == 0 && pressedKey.Key == Enter)
                {
                    break;
                }

                if (pressedKey.Key != RightArrow &&
                    pressedKey.Key != LeftArrow &&
                    pressedKey.Key != UpArrow &&
                    pressedKey.Key != DownArrow
                )
                {
                    secretPatternMatch.Clear();

                    confirmedRegNr += (char)pressedKey.Key;
                    confirmedRegNr = confirmedRegNr.Replace("\r", string.Empty);

                    //if (confirmedRegNr.Length > 7)
                    //{
                    //    confirmedRegNr = "";
                    //    Console.Clear();
                    //    Helper.DisplayHelper.DisplayRed("Registration number must be less than 8 characters!");
                    //    MainMenu.PressAnyKeyToContinue();
                    //    break;
                    //}

                    //if (confirmedRegNr.Length < 2 && pressedKey.Key == Enter)
                    //{
                    //    confirmedRegNr = "";
                    //    Console.Clear();
                    //    Helper.DisplayHelper.DisplayRed("Registration number cannot be empty or less than 2 characters!");
                    //    MainMenu.PressAnyKeyToContinue();
                    //    continue;
                    //}

                    if (confirmedRegNr.Length >= 2 && confirmedRegNr.Length <= 7 && pressedKey.Key == Enter)
                    {
                        isValidRegNr = true;
                        MainMenu.regNr = confirmedRegNr.Replace("\r", string.Empty);
                        confirmedRegNr = "";
                        break;
                    }
                }
                else
                {
                    secretPatternMatch.Add(pressedKey.Key);
                }
                CheckSecretPatternMatch();
                if (hasAdminLoggedOut)
                {
                    break;
                }
            }

            secretPatternMatch.Clear();
        }

        private static void CheckSecretPatternMatch()
        {
            if (secretPatternMatch.SequenceEqual(secretPattern))
            {
                AdminLoginUnlocked();
            }
        }

        private static void AdminLoginUnlocked()
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
                MainMenu.PressAnyKeyToContinue();
            }
        }
    }
}