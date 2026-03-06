using Act7Obj.Model;
using Act7Obj.View;
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
        private static void SavePlayerData(Player myPlayer, PlayerCharacterModel selectedHero)
        {
            // SYNC: Keep your logic for copying hero stats to player object
            myPlayer.CharacterName = selectedHero.CharacterName;
            myPlayer.CharacterType = selectedHero.CharacterType;
            myPlayer.CharacterDescription = selectedHero.CharacterDescription;
            myPlayer.Health = selectedHero.Health;
            myPlayer.MaxHealth = selectedHero.MaxHealth;
            myPlayer.AttackDamage = selectedHero.AttackDamage;
            myPlayer.CritChance = selectedHero.CritChance;
            myPlayer.CritDamage = selectedHero.CritDamage;
            myPlayer.Intelect = selectedHero.Intelect;
            myPlayer.Speed = selectedHero.Speed;
            myPlayer.PlayerLevel = selectedHero.PlayerLevel;
            myPlayer.PlayerGold = selectedHero.PlayerGold;

            myPlayer.SkillNames = selectedHero.StartingDeck.ConvertAll(card => card.Name);
            myPlayer.StartingDeck = new List<CardModel>(selectedHero.StartingDeck);
            myPlayer.SkillDescriptions = selectedHero.SkillDescriptions;
            myPlayer.PassiveSkills = selectedHero.PassiveSkills;
            myPlayer.PassiveDescriptions = selectedHero.PassiveDescriptions;

            // Database Call
            DatabaseService.SavePlayerData(myPlayer);
            Console.WriteLine("Saving player data...");
            Console.WriteLine("Successfully saved player data!");
        }

        public static Player? UserInputFunction()
        {
            while (true)
            {
                string choice = ConsoleInterface.DisplayGameMenu();

                switch (choice)
                {
                    case "1": // NEW GAME logic remains the same
                        Console.Clear();
                        Player myPlayer = ConsoleInterface.DisplaySetPlayerName();
                        PlayerCharacterModel? selectedHero = ConsoleInterface.DisplayCharacterSelection();

                        if (selectedHero != null)
                        {
                            myPlayer.SelectedHero = selectedHero;
                            Console.Clear();
                            ConsoleInterface.DisplayPlayerStats(selectedHero, myPlayer);
                            ConsoleInterface.DisplayCreatedMessage();
                            SavePlayerData(myPlayer, selectedHero);
                            TextMoveInUIController.BottomRightPromptContinue();
                            return myPlayer;
                        }
                        break;

                    case "2": // CONTINUE GAME
                        List<string> savedNames = DatabaseService.GetAllSavedPlayerNames();

                        if (savedNames.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
                            TextMoveInUIController.CenterText("║       NO SAVED DATA DETECTED             ║");
                            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.ReadKey();
                        }
                        else
                        {
                            bool stayInContinueMenu = true;
                            while (stayInContinueMenu)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
                                TextMoveInUIController.CenterText("║          LOAD YOUR PROGRESS              ║");
                                TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.WriteLine("\n");

                                for (int i = 0; i < savedNames.Count; i++)
                                {
                                    Console.WriteLine($"    [{i + 1}] PLAYER: {savedNames[i].PadRight(15)} | Status: READY");
                                }
                                Console.WriteLine($"    [{savedNames.Count + 1}] RETURN TO MAIN MENU");
                                Console.WriteLine("\n" + new string('─', Console.WindowWidth)); 
                                Console.Write("    Select Save Index: ");
                                string input = Console.ReadLine()!;

                                if (int.TryParse(input, out int inputIdx) && inputIdx > 0 && inputIdx <= savedNames.Count)
                                {
                                    string selectedName = savedNames[inputIdx - 1];
                                    Player? loadedPlayer = DatabaseService.LoadPlayerData(selectedName);

                                    if (loadedPlayer != null)
                                    {
                                        // 1. Properly instantiate the specific Hero class to get the Cards
                                        PlayerCharacterModel baseHero = loadedPlayer.CharacterType switch
                                        {
                                            "Archer" => new Aristain(),
                                            "Mage" => new Paul(),
                                            "Tank" => new Sumayang(),
                                            "Fighter" => new Pascual(),
                                            _ => new Aristain() 
                                        };

                                        // 2. Map the SAVED stats onto the new hero instance
                                        baseHero.CharacterName = loadedPlayer.CharacterName;
                                        baseHero.Health = loadedPlayer.Health;
                                        baseHero.MaxHealth = loadedPlayer.MaxHealth;
                                        baseHero.AttackDamage = loadedPlayer.AttackDamage;
                                        baseHero.CritChance = loadedPlayer.CritChance;
                                        baseHero.CritDamage = loadedPlayer.CritDamage;
                                        baseHero.Intelect = loadedPlayer.Intelect;
                                        baseHero.Speed = loadedPlayer.Speed;
                                        baseHero.PlayerLevel = loadedPlayer.PlayerLevel;
                                        baseHero.PlayerGold = loadedPlayer.PlayerGold;
                                        
                                        // 3. Assign the "Smart" hero back to the player
                                        loadedPlayer.SelectedHero = baseHero;

                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        TextMoveInUIController.CenterText(">> DATA RETRIEVED SUCCESSFULLY <<");
                                        Console.ResetColor();

                                        ConsoleInterface.DisplayPlayerStats(loadedPlayer.SelectedHero, loadedPlayer);
                                        TextMoveInUIController.BottomRightPromptContinue();
                                        return loadedPlayer; 
                                    }
                                }
                                else if (inputIdx == savedNames.Count + 1)
                                {
                                    stayInContinueMenu = false;
                                }
                                else
                                {
                                    ShowInvalidMessageView.ShowInvalidMessage();
                                }
                            }
                        }
                        break;

                    case "3":
                        // EXIT logic remains the same
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}