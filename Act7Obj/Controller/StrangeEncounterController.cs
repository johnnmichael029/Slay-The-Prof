using Slay_The_Prof.View;
using Slay_The_Prof.Service;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.EnemyModel;
using System;
using System.Collections.Generic;
using System.Text;
using Act7Obj.Model;
using Act7Obj.Controller;

namespace Slay_The_Prof.Controller
{
    public class StrangeEncounterController
    {
        public static void StrangeManEncounter(Player player)
        {           
           bool hasEncounteredStrangeMan = true; 
           while (hasEncounteredStrangeMan)
            {
                string action = PlayerGameStoryView.StrangeEncounterMessage();
                switch (action)
                {
                    case "1":
                        // Player accepts the stranger's offer, receives rewards, and saves data
                        PlayerGameStoryView.StrangeEncounterHappy();
                        RewardController.GetRewardsFromStrangeManAndSaveData(player);
                        return;
                    case "2":
                        // Player declines the stranger's offer and proceeds to battle
                        InitializeEnemyBeforeBattleAndCard.InitializeStrangeManAndCards(player);
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please choose 1 or 2.");
                        break;
                }
            }          
        }     
    }
}
