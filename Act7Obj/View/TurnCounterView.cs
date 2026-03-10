using Act7Obj.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public class TurnCounterView
    {
        public static void DisplayTurnCounter(Player player, int currentEnergy, int turnCounter)
        {
            int dynamicEnergy = player.PlayerEnergy;
            Console.Clear();
            // 1. Header with Turn and Energy
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║            TURN {turnCounter}                                        ║");
            
            // Visual Energy Bar
            string energyPoints = new string('o', currentEnergy) + new string(' ', currentEnergy - currentEnergy);
            Console.WriteLine($"║            ENERGY: [{energyPoints}] {currentEnergy}/{dynamicEnergy}                            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
