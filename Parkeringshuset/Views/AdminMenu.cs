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
        /// Prints admin menu
        /// </summary>
        public void PrintAdminPage()
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
                switch (choice)
                {
                    case 1:
                        afl.ParkingSportsPopularity();
                        break;

                    case 2:
                        afl.SoldTicketsBetweenSpecificDates();
                        break;

                    case 3:
                        //afl.Revenue();
                        break;

                    case 4:
                        //afl.Revenue();
                        break;

                    case 5:
                        afl.BestCustomer();
                        break;

                    case 6:
                        afl.comprehensiveReport();
                        break;

                    case 7:
                        isRunning = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Asks for admin credentials
        /// </summary>
        /// <returns>True if admin is found</returns>
        public bool LoginAdmin()
        {
            Console.Clear();
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            LoginController lc = new();
            return lc.IsLoginSuccessful(username, password);
        }
    }
}