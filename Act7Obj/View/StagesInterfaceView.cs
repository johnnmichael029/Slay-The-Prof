using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Controller;
using Slay_The_Prof.Controller.TraderController;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.EnemyModel;
using Slay_The_Prof.Service;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Slay_The_Prof.View
{
    public class StagesInterfaceView
    {
        public static void ShowFirstStagesInterfaces(Player currentPlayer)
        {
            bool gameIsRunning = true;
            while (gameIsRunning)
            {
                string action = PlayerGameStoryView.CantindogsStory();
                switch (action)
                {
                    case "1":
                        // Fight
                        InitializeEnemyBeforeBattleAndCard.InitializeCatindogsAndCards(currentPlayer);
                        return;

                    case "2":
                        // Attempt to Escape
                        bool escapeResult = RNGController.RollDiceForPlayerEscape(currentPlayer);

                        if (escapeResult)
                        {
                            Console.Clear();
                            int damage = (int)(currentPlayer.MaxHealth * 0.15);
                            currentPlayer.Health -= damage;
                            currentPlayer.ClassBattle += 1;

                            // Ensure health doesn't drop below 0
                            if (currentPlayer.Health < 0) currentPlayer.Health = 0;

                            // 2. Save the new Health state to Database
                            DatabaseService.SavePlayerData(currentPlayer);

                            Console.WriteLine("\n");
                            TextMoveInUIController.CenterText($"The fear of Notepad cost you {damage} HP.");
                            Console.ForegroundColor = ConsoleColor.Green;
                            TextMoveInUIController.CenterText($"Current HP: {currentPlayer.Health}/{currentPlayer.MaxHealth}");
                            Console.ResetColor();
                            TextMoveInUIController.CenterText(">> PROGRESS AUTO-SAVED <<");
                            TextMoveInUIController.BottomRightPromptContinue();

                            if (currentPlayer.Health <= 0)
                            {
                                BattleController.EndBattleIfLoseThenSaveProgress(currentPlayer);
                            }
                        }
                        else
                            InitializeEnemyBeforeBattleAndCard.InitializeCatindogsAndCards(currentPlayer);
                        return;

                    case "3": // Return to Main Menu
                        TextMoveInUIController.CenterText("Returning to Main Menu...");
                        gameIsRunning = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void ShowStrangeEncounterInterface(Player currentPlayer)
        {
            bool validChoice = true;
            while (validChoice)
            {
                string action = PlayerGameStoryView.StrangeEncounter();
                switch (action)
                {
                    case "1": // Accept Offer
                        PlayerGameStoryView.StrangeEncounterHappy();
                        RewardController.GetRewardsFromStrangeManAndSaveData(currentPlayer);
                        return;

                    case "2": // Escape
                        bool escapeResult = RNGController.RollDiceForPlayerEscape(currentPlayer);

                        if (escapeResult)
                        {
                            currentPlayer.ClassBattle += 1;
                            DatabaseService.SavePlayerData(currentPlayer);
                            ShowStage1Battle2Interface(currentPlayer);
                        }
                        else
                        {
                            StrangeEncounterController.StrangeManEncounter(currentPlayer);
                        }

                        return;

                    case "3": // Return to Main Menu
                        TextMoveInUIController.CenterText("Returning to Main Menu...");
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void ShowStage1Battle2Interface(Player currentPlayer)
        {

            bool validChoice = true;
            while (validChoice)
            {
                string action = PlayerGameStoryView.TrinityStory();
                switch (action)
                {
                    case "1": // Accept Offer
                        PlayerGameStoryView.TrinityMessage();
                        InitializeEnemyBeforeBattleAndCard.InitializeTrinityAndCards(currentPlayer);
                        return;

                    case "2": // Escape
                        bool escapeResult = RNGController.RollDiceForPlayerEscape(currentPlayer);

                        if (escapeResult)
                        {
                            currentPlayer.ClassBattle += 1;
                            DatabaseService.SavePlayerData(currentPlayer);
                        }
                        else
                        {
                            InitializeEnemyBeforeBattleAndCard.InitializeTrinityAndCards(currentPlayer);
                        }

                        return;

                    case "3": // Return to Main Menu
                        TextMoveInUIController.CenterText("Returning to Main Menu...");
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void ShowStage1Battle3Interface(Player currentPlayer)
        {
            if (currentPlayer.ClassBreak == 1)
            {
                StagesControlling.ClassBreak1(currentPlayer);
                return;
            }
            Console.WriteLine("end of program...");
        }

        public static void ShowClassBreak1Interface(Player currentPlayer)
        {
            bool validChoice = true;
            while (validChoice)
            {
                string action = PlayerGameStoryView.ClassBreak1();
                switch (action)
                {
                    case "1": // Buy Items
                        BuyItemController.GiveBuyItems(currentPlayer);
                        return;
                    case "2": // Skip
                        return;
                    case "3": // Skip
                        currentPlayer.ClassBreak += 1;
                        DatabaseService.SavePlayerData(currentPlayer);
                        ShowStage1Battle3Interface(currentPlayer);
                        return;
                    case "4": // Return to Main Menu
                        TextMoveInUIController.CenterText("Returning to Main Menu...");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
