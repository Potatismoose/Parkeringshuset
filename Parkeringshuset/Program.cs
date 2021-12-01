using Parkeringshuset.Helpers;
using Parkeringshuset.Models;
using Parkeringshuset.Views;

namespace Parkeringshuset
{
    static class Program
    { 
        static void Main(string[] args)
        {
            SeedData.RunMock();
           

            //ParkingGarage pg = new(12);
            //Menu menu = new(pg);
            //menu.PrintMainMenu();
            //Login.PrintLoginPage();
            //MainMenu.RunMainMenu();
        }
    }
}
