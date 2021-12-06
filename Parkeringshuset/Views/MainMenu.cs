using System.Collections.Generic;
using System;

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
            if (ParkingMeterLogic.CheckIn(regNr))
            {

            }


        }
    }
}
