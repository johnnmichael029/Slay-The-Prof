using Act7Obj.Model;
using Act7Obj.View;
using Slay_The_Prof.Controller;
using Slay_The_Prof.Model;
using Slay_The_Prof.Service;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;

// ... existing using statements ...

namespace Act7Obj.Controller
{
    public class UserInputController
    {


        public static Player? UserInputFunction()
        {
            while (true)
            {
                string choice = ConsoleInterface.DisplayGameMenu();

                switch (choice)
                {
                    case "1":
                        NewGameController.NewGameData();
                        break;
                    case "2": // CONTINUE GAME
                        List<string> names = DatabaseService.GetAllSavedPlayerNames();
                        if (names.Count > 0)
                        {
                            // Catch the returned player object here!
                            Player? continuedPlayer = LoadPlayerDataController.LoadPlayerData(names);
                            if (continuedPlayer != null)
                            {
                                return continuedPlayer; // Start the game with the loaded player
                            }
                        }
                        NoSaveDataView.NoSaveGameData();
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;
                        
                }
            }
        }
    }
}