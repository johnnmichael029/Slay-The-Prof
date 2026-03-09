using Act7Obj.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public class EscapeView
    {
        public static void EscapeSuccessfuInterface()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║            ESCAPE SUCCESSFUL!            ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            TextMoveInUIController.BottomRightPromptContinue();
        }
        public static void EscapeFailedInterface()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║              ESCAPE FAILED!              ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            TextMoveInUIController.BottomRightPromptContinue();

        }       
    }
}
