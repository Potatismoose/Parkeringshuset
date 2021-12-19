using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Controllers;
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
                Console.WriteLine("1. Get sold tickets between specific dates");
                Console.WriteLine("3. Get total revenue between specific dates");
                Console.WriteLine("4. Get total revenue by fiscal year");
                Console.WriteLine("5. Get best costumer");
                Console.WriteLine("6. Get comprehensive report");
                Console.WriteLine("7. Logout");
                int.TryParse(Console.ReadLine(), out int choice);
                DateTime startDate;
                DateTime endDate;
                switch (choice)
                {
                    case 1:
                        //afl.ParkingSportsPopularity(admin);
                        break;

                    case 2:
                        //afl.SoldTicketsBetweenSpecificDates(admin, startDate, endDate);
                        break;

                    case 3:

                        startDate = AskForStartOrEndDate("Start");
                        endDate = AskForStartOrEndDate("End");
                        Console.WriteLine(startDate);
                        Console.WriteLine(endDate);
                        Console.ReadKey();
                        //afl.Revenue(admin, startDate, endDate);
                        break;

                    case 4:
                        //afl.Revenue();
                        break;

                    case 5:
                        //afl.BestCustomer(admin);
                        break;

                    case 6:
                        //afl.comprehensiveReport();
                        break;

                    case 7:
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
            do
            {
                Console.Write($"{startOrEnd}date - Year (YYYY): ");
                isNumber = int.TryParse(Console.ReadLine(), out int year);
                Year = year;
                
            } while (!isNumber || Year < 1950 || Year > DateTime.Now.Year);
            Console.WriteLine(DateTime.Now.Year);

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
            } while (!isNumber || Day > DateTime.DaysInMonth(Year,Month));

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