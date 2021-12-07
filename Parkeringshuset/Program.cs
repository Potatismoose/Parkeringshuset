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
            SeedData.RunMock();
            MainMenu.RunMainMenu();
        }
    }
}
