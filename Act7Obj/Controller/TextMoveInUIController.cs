using System;
using System.Collections.Generic;
using System.Text;

namespace Act7Obj.Controller
{
    public class TextMoveInUIController
    {
        // Logic to center text based on current window size
        public static void CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;
            int stringWidth = text.Length;
            int spaces = (screenWidth / 2) - (stringWidth / 2);

            // Ensure we don't have negative spaces if window is too small
            Console.WriteLine(new string(' ', Math.Max(0, spaces)) + text);
        }

        // Logic to move text to the bottom right corner
        public static void BottomRightPromptContinue()
        {
            string prompt = "Press any key to continue...";

            // Set cursor to the last row, aligned to the right
            int row = Console.WindowHeight - 1;
            int col = Console.WindowWidth - prompt.Length - 1;

            try
            {
                Console.SetCursorPosition(Math.Max(0, col), Math.Max(0, row));
                Console.Write(prompt);
            }
            catch
            {
                // Fallback if window is too small for SetCursorPosition
                Console.WriteLine("\n" + prompt);
            }

            Console.ReadKey(true); // 'true' hides the key you pressed from the screen
        }
    }
}
