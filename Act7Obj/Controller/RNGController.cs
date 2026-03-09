using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Controller;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class RNGController
    {
        public static bool RollDiceForPlayerEscape(Player player)
        {
            bool escapeSuccessful = false;
            Random random = new();
            int diceRoll = random.Next(1, 100); // Simulate a 6-sided dice roll (1-6)
            if (diceRoll <= player.Speed) 
            {
                EscapeView.EscapeSuccessfuInterface();
                escapeSuccessful = true;
            }
            else
            {
                EscapeView.EscapeFailedInterface();
            }
            return escapeSuccessful;
        }
    }
}
