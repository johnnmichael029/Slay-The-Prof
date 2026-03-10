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

                Action stagesClassBattle = currentPlayer.ClassBattle switch
                {
                    0 => () => StagesInterfaceView.ShowFirstStagesInterfaces(currentPlayer),
                    1 => () => StagesInterfaceView.ShowStrangeEncounterInterface(currentPlayer),
                    2 => () => StagesInterfaceView.ShowStage1Battle2Interface(currentPlayer),
                    3 => () => StagesInterfaceView.ShowStage1Battle3Interface(currentPlayer),
                    _ => () =>
                    {
                        Console.WriteLine("End of current demo. Stay tuned for more classes!");
                        programRunning = false;
                    }
                    
                };
                stagesClassBattle();            
            }
        }
    }
}
