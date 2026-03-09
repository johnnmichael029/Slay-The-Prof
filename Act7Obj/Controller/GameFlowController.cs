using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class GameFlowController
    {
        public static void GameFlow()
        {
            bool programRunning = true;
            while (programRunning)
            {
                Player? currentPlayer = UserInputController.UserInputFunction();

                // If player is null, they chose to exit the menu
                if (currentPlayer == null)
                {
                    programRunning = false;
                    break;
                }

                // Use a Switch to direct the player to the correct "Class"
                switch (currentPlayer.ClassBattle) // Assuming ClassBattle is now CurrentStage
                {
                    case 0: // Catindogs Story
                        StagesInterfaceView.ShowFirstStagesInterfaces(currentPlayer);
                        break;

                    case 1: // Strange Encounter Story
                        StagesInterfaceView.ShowStrangeEncounterInterface(currentPlayer);
                        break;

                    case 2: // Trinity Story
                        StagesInterfaceView.ShowStage1Battle2Interface(currentPlayer);
                        break;

                    default:
                        // If they reach a stage you haven't coded yet
                        Console.WriteLine("End of current demo. Stay tuned for more classes!");
                        programRunning = false;
                        break;
                }
            }
        }
    }
}
