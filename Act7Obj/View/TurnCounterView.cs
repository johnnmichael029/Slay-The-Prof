using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public class TurnCounterView
    {
        public static void DisplayTurnCounter(int currentEnergy, int turnCounter)
        {
            
            Console.Clear();
            // 1. Header with Turn and Energy
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║            TURN {turnCounter}                                        ║");

            // Visual Energy Bar
            string energyPoints = new string('o', currentEnergy) + new string(' ', 3 - currentEnergy);
            Console.WriteLine($"║            ENERGY: [{energyPoints}] {currentEnergy}/3                             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
