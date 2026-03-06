using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public class ShowInvalidMessageView
    {
        public static void ShowInvalidMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n    [!] Invalid selection. Please choose a valid index.");
            Console.ResetColor();
            Console.WriteLine("    Press any key to try again...");
            Console.ReadKey();
        }
    }
}
