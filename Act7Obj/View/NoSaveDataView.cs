using Act7Obj.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public class NoSaveDataView
    {
        public static void NoSaveGameData()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║       NO SAVED DATA DETECTED             ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            TextMoveInUIController.BottomRightPromptContinue();
        }
    }
}
