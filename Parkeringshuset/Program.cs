using Parkeringshuset.Helpers;
using Parkeringshuset.Helpers.TicketHelper;
using Parkeringshuset.Models;
using Parkeringshuset.Views;
using System;

namespace Parkeringshuset
{
    static class Program
    { 
        static void Main(string[] args)
        {
            PrintingHelper.PhysicalTicketCreationAndPrintout(
                new PTicket()
                {
                    CheckedInTime = new DateTime(2021, 12, 03, 14, 40, 03),
                    Vehicle = new Vehicle()
                    {
                        RegistrationNumber = "NPK144"
                    },
                    Type = new PType()
                    {
                        Name = "Handicap"
                    }
                });

            SeedData.RunMock();
        }
    }
}
