namespace Parkeringshuset.Helper
{
    using System;

    public static class DisplayHelper
    {
        /// <summary>
        /// Changes console color to red in case of wrong input.
        /// </summary>
        /// <param name="text"></param>
        public static void DisplayRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Changes console color to green in case of correct input.
        /// </summary>
        /// <param name="text"></param>
        public static void DisplayGreen(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}