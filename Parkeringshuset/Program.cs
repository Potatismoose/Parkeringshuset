using Parkeringshuset.Helpers;
using Parkeringshuset.Views;

namespace Parkeringshuset
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            SeedData.RunMock();
            SeedData.CreateAdmin();
            MainMenu.RunMainMenu();
            //Test
        }
    }
}