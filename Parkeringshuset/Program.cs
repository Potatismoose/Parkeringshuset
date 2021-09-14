using Parkeringshuset.Views;

namespace Parkeringshuset
{
    static class Program
    {
        static void Main(string[] args)
        {
            Login.PrintLoginPage();
            MainMenu.RunMainMenu();
        }
    }
}
