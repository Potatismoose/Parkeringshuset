using Parkeringshuset.BusinessLogic;
using Parkeringshuset.Controllers;
using Parkeringshuset.Helper;
using Parkeringshuset.Helpers.TicketHelper;
using Parkeringshuset.Models;
using System;

namespace Parkeringshuset.Views
{
    public static class MainMenu
    {
        public static string regNr;
        private static ParkingTypeController TypeController = new();
        private static ParkingTicketController TicketController = new();

        /// <summary>
        /// Check in and out menu for users of the parking garage.
        /// </summary>
        public static void RunMainMenu()
        {
            bool keepGoing = true;
            string Ptype = "";

            ParkingTicketController pTC = new();
            PaymentLogic pL = new();
            do
            {
                regNr = "";
                Console.Clear();
                Console.Write("Enter your registration number: ");

                MenuHelper.UserInput();

                if (string.IsNullOrEmpty(regNr))
                {
                    continue;
                }

                var parkingTicket = TicketController.GetActiveTicket(regNr);

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
                            Ptype = "Regular";
                            CheckIn(regNr, Ptype);
                            MenuHelper.PressAnyKeyToContinue();
                            break;

                        case 2:
                            Ptype = "Electric";
                            CheckIn(regNr, Ptype);
                            MenuHelper.PressAnyKeyToContinue();
                            break;

                        case 3:
                            Ptype = "Handicap";
                            CheckIn(regNr, Ptype);
                            MenuHelper.PressAnyKeyToContinue();
                            break;
                        //case 4:
                        //    TicketControllerype = "Monthly";
                        //    TicketControllerL.CheckIn(regNr, TicketControllerype);
                        //    PressAnyKeyToContinue();
                        //    break;
                        case 5:
                            Ptype = "Motorbike";
                            CheckIn(regNr, Ptype);
                            MenuHelper.PressAnyKeyToContinue();
                            break;

                        case 6:

                            continue;

                        default:
                            Console.WriteLine("Jerry created a problem, please try again!");
                            MenuHelper.PressAnyKeyToContinue();
                            break;
                    }
                }
                else if (TicketController.IsMonthly(parkingTicket) && TicketController.IsTicketActive(parkingTicket))
                {
                    Console.WriteLine($"Welcome back! Your ticket expires " +
                        $"{ parkingTicket.CheckedOutTime}");
                    MenuHelper.PressAnyKeyToContinue();
                }
                else if (TicketController.IsTicketActive(parkingTicket))
                {
                    Console.Clear();
                    CreditCard cC = new CreditCard();
                    Console.Write("Please provide creditcard number: ");
                    cC.Number = Console.ReadLine();
                    Console.Write("Please provide csv number: ");
                    cC.CSV = Console.ReadLine();
                    pTC.CheckOut(parkingTicket);
                    var ticket = pL.Payment(cC, parkingTicket);
                    ticket.isActice = false;

                    pTC.Update(ticket);

                    if (ticket.IsPaid)
                    {
                        DisplayHelper.DisplayGreen("Payment is done.");
                        Console.WriteLine($"Total cost of ticket was {ticket.Cost} SEK");
                        Console.WriteLine("Checked out. Thank you for using our garage, welcome back!");
                        PressAnyKeyToContinue();
                    }
                    else
                    {
                        Console.WriteLine($"Invoice sent to registred address of car {ticket.Vehicle.RegistrationNumber}" +
                            $" with cost of {ticket.Cost} SEK");
                        PressAnyKeyToContinue();
                    }
                }
            } while (keepGoing);
        }

        public static bool CheckIn(string regNr, string TicketControllerype)
        {
            if (TypeController.ReadFreeSpots(TicketControllerype) > 0)
            {
                if (TicketController.CreateTicket(regNr, TicketControllerype))
                {
                    var ticket = TicketController.GetActiveTicket(regNr);

                    if (ticket is not null)
                    {
                        DisplayHelper.DisplayGreen("Ticket is activated. Welcome!");
                        PrintingHelper.PhysicalTicketCreationAndPrintout(ticket);       // TODO: Need to add +1 to UsedSpots i TicketControllerypes table.
                        return true;
                    }
                }
            }
            else
            {
                DisplayHelper.DisplayRed("There is no available parking spots for this type.");
                return false;
            }

            DisplayHelper.DisplayRed("Check in failed, try again or contact our support");
            return false;
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