using Parkeringshuset.Helpers;
using Parkeringshuset.Views;

namespace Parkeringshuset
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            SeedDataHelper.RunMock();
            SeedDataHelper.CreateAdmin();
            MainMenu.RunMainMenu();
            //Test
        }
    }
}