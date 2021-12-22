using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Controllers;
using Parkeringshuset.Helper;
using Parkeringshuset.Models;
using System;
using System.Collections.Generic;

namespace Parkeringshuset.Views
{
    public class AdminMenu
    {
        /// <summary>
        /// Prints admin menu.
        /// </summary>
        public void PrintAdminPage(Admin admin)
        {
            AdminFunctionsLogic afl = new();
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Get parking type popularity");
                Console.WriteLine("2. Get sold tickets between specific dates");
                Console.WriteLine("3. Get total revenue between specific dates");
                Console.WriteLine("4. Get total revenue by fiscal year");
                Console.WriteLine("5. Get best costumer");
                Console.WriteLine("6. Logout");
                
                int.TryParse(Console.ReadLine(), out int choice);
                DateTime startDate;
                DateTime endDate;
                switch (choice)
                {
                    case 1:
                        var listOfParkingspotPopularity = afl.ParkingSpotsPopularity(admin);
                        foreach (var item in listOfParkingspotPopularity)
                        {
                            Console.WriteLine($"Parkingtype: {item.Item2}");
                            Console.WriteLine($"Sold ticket: {item.Item1}");
                            Console.WriteLine("-----------------------------");
                        }
                        MenuHelper.PressAnyKeyToContinue();

                        break;

                    case 2:
                        startDate = AskForStartOrEndDate("Start");
                        endDate = AskForStartOrEndDate("End");
                        var amoutOfSoldTickets = afl.SoldTicketsBetweenSpecificDates(admin, startDate, endDate);
                        DisplayHelper.DisplayGreen($"Number of sold tickets between " +
                            $"{startDate.ToShortDateString()} & " +
                            $"{endDate.ToShortDateString()} was {amoutOfSoldTickets}");
                        MenuHelper.PressAnyKeyToContinue();
                        break;

                    case 3:

                        startDate = AskForStartOrEndDate("Start");
                        endDate = AskForStartOrEndDate("End");
                        if (afl.Revenue(admin, startDate, endDate))
                        {
                            DisplayHelper.DisplayGreen($"E-mail is sent to {admin?.Email}");
                            MenuHelper.PressAnyKeyToContinue();
                        }
                        else
                        {
                            DisplayHelper.DisplayRed($"E-mail was not sent, something went wrong.");
                            MenuHelper.PressAnyKeyToContinue();
                        }
                        
                        break;

                    case 4:
                        startDate = AskForStartOrEndDate("Fiscal");
                        afl.Revenue(admin, startDate);
                        break;

                    case 5:
                        var bestCustomer = afl.GetCustomerAndMoneySpent(admin);
                        if (bestCustomer.Count > 0)
                        {
                            for (int i = 0; i < bestCustomer.Count; i++)
                            {
                                if (i > 9)
                                    break;
                                Console.WriteLine($"Money spent: {bestCustomer[i].Item1} sek \n" +
                                    $"Registration nr: {bestCustomer[i].Item2}");
                                Console.WriteLine("--------------------------");
                            }
                        }
                        else
                        {
                            DisplayHelper.DisplayRed("No vehicles have parked in the garage.");
                        }
                        MenuHelper.PressAnyKeyToContinue();
                        break;

                    case 6:
                        isRunning = false;
                        break;
                        
                }
            }
        }

        /// <summary>
        /// Return correct date for a ticket.
        /// </summary>
        /// <param name="startOrEnd">Takes "Start" or "End" as a string for defining 
        /// if the input is for startdate or enddate</param>
        /// <returns>Returns datetime</returns>
        private static DateTime AskForStartOrEndDate(string startOrEnd)
        {
            int Year;
            int Month;
            int Day;
            bool isNumber;
            if (startOrEnd == "Fiscal")
            {
                do
                {
                    Console.Write($"{startOrEnd}year - (YYYY): ");
                    isNumber = int.TryParse(Console.ReadLine(), out int year);
                    Year = year;

                } while (!isNumber || Year < 1950 || Year > DateTime.Now.Year);
                Day = 1;
                Month = 1;
            }
            else
            {
                do
                {
                    Console.Write($"{startOrEnd}date - Year (YYYY): ");
                    isNumber = int.TryParse(Console.ReadLine(), out int year);
                    Year = year;

                } while (!isNumber || Year < 1950 || Year > DateTime.Now.Year);
                

                do
                {
                    Console.Write($"{startOrEnd}date - Month (MM): ");
                    isNumber = int.TryParse(Console.ReadLine(), out int month);
                    Month = month;
                } while (!isNumber || Month > 12 || Month < 1);

                do
                {
                    Console.Write($"{startOrEnd}date - Day (DD): ");
                    isNumber = int.TryParse(Console.ReadLine(), out int day);
                    Day = day;
                } while (!isNumber || Day > DateTime.DaysInMonth(Year, Month));
            }
            

            return new DateTime(Year, Month, Day);
        }

        /// <summary>
        /// Asks for admin credentials.
        /// </summary>
        /// <returns>True if admin is found</returns>
        public Admin LoginAdmin()
        {
            Console.Clear();
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();
            LoginController lc = new();
            return lc.LoginReturnAdmin(username, password);
        }
    }
}