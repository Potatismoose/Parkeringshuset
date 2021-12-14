using Parkeringshuset.Helpers;
using Parkeringshuset.Helpers.TicketHelper;
using Parkeringshuset.Models;
using Parkeringshuset.Views;
using System;

namespace Parkeringshuset
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            SeedData.RunMock();
            SeedData.CreateAdmin();
            MainMenu.RunMainMenu();
        }
    }
}