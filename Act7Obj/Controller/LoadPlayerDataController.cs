using Act7Obj.Controller;
using Act7Obj.Model;
using Act7Obj.View;
using Slay_The_Prof.Service;
using Slay_The_Prof.View;
using Slay_The_Prof.Model.CharacterModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class LoadPlayerDataController
    {
        public static Player? LoadPlayerData(List<string> savedNames)
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

                        foreach (string skillName in loadedPlayer.SkillNames)
                        {
                            var card = CardLibraryService.GetCardByName(skillName);
                            if (card != null)
                            {
                                baseHero.StartingDeck.Add(card);
                            }
                        }

                        // 2. Map the SAVED stats onto the new hero instance
                        baseHero.CharacterName = loadedPlayer.CharacterName;
                        baseHero.Health = loadedPlayer.Health;
                        baseHero.MaxHealth = loadedPlayer.MaxHealth;
                        baseHero.AttackDamage = loadedPlayer.AttackDamage;
                        baseHero.CritChance = loadedPlayer.CritChance;
                        baseHero.CritDamage = loadedPlayer.CritDamage;
                        baseHero.IntelLect = loadedPlayer.IntelLect;
                        baseHero.Speed = loadedPlayer.Speed;
                        baseHero.PlayerLevel = loadedPlayer.PlayerLevel;
                        baseHero.CurrentExp = loadedPlayer.CurrentExp;
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
            return null; // Safety return
        }
    }
}
