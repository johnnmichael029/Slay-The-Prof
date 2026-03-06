using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Controller;
using Slay_The_Prof.Model;
using Slay_The_Prof.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public  class StagesInterfaceView
    {
        public static void ShowFirsttagesInterfaces(Player currentPlayer)
        {
            bool gameIsRunning = true;
            while (gameIsRunning)
            {
                Console.Clear();
                // Header for Stage 1
                Console.ForegroundColor = ConsoleColor.Yellow;
                TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
                TextMoveInUIController.CenterText("║          STAGE 1: FIRST SEMESTER         ║");
                TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
                Console.ResetColor();
                TextMoveInUIController.CenterText("Class Battle 1");
                Console.WriteLine("\n");

                // Story Text
                TextMoveInUIController.CenterText("It is your very first day of class for BSIS.");
                TextMoveInUIController.CenterText("The hallway is quiet as you enter the classroom...");
                TextMoveInUIController.CenterText("Suddenly, a wild PROF. CANTINDOGS appears!");
                Console.WriteLine("\n");

                Console.ForegroundColor = ConsoleColor.Red;
                TextMoveInUIController.CenterText("\"CANTINDOGS - Prepare for your Program in Notepad!\"");
                Console.ResetColor();
                Console.WriteLine("\n");

                Console.WriteLine("What will you do?");
                Console.WriteLine("[1] Fight (Do the Program)");
                Console.WriteLine("[2] Escape (Skip Cantindogs Class)");
                Console.WriteLine("[3] Return to Main Menu");
                Console.WriteLine();

                Console.Write("Your Action: ");
                string action = Console.ReadLine()!;

                switch (action)
                {
                    case "1": // Fight
                        Console.Clear();
                        Enemy boss = new CantindogsCharacterModel();

                        if (currentPlayer.SelectedHero != null)
                        {
                            currentPlayer.StartingDeck = currentPlayer.SelectedHero.StartingDeck;
                        }

                        // Now the deck won't be empty!
                        CardManagerController deck = new CardManagerController(currentPlayer.StartingDeck);
                        // Note: BattleLoop calls DrawCards(4) inside it, so you don't need to call it here.
                        BattleController.BattleControlling(currentPlayer, boss, deck);
                        return;

                    case "2": // ESCAPE WITH SAVE
                        Console.Clear();
                        // 1. Calculate and Apply Damage
                        int damage = (int)(currentPlayer.MaxHealth * 0.15);
                        currentPlayer.Health -= damage;

                        // Ensure health doesn't drop below 0
                        if (currentPlayer.Health < 0) currentPlayer.Health = 0;

                        // 2. Save the new Health state to Database
                        DatabaseService.SavePlayerData(currentPlayer);

                        Console.ForegroundColor = ConsoleColor.Red;
                        TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
                        TextMoveInUIController.CenterText("║            YOU HAVE FLED!                ║");
                        TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
                        Console.ResetColor();

                        Console.WriteLine("\n");
                        TextMoveInUIController.CenterText($"The fear of Notepad cost you {damage} HP.");
                        TextMoveInUIController.CenterText($"Current HP: {currentPlayer.Health}/{currentPlayer.MaxHealth}");
                        TextMoveInUIController.CenterText(">> PROGRESS AUTO-SAVED <<");
                        TextMoveInUIController.BottomRightPromptContinue();

                        if (currentPlayer.Health <=0)
                        {
                            BattleController.EndBattleIfLoseThenSaveProgress(currentPlayer);
                        }
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
     
        public static void DrawHealthBar(int current, int max, ConsoleColor color)
        {
            int barWidth = 30;
            float percentage = max > 0 ? (float)current / max : 0;
            int filled = (int)(percentage * barWidth);

            Console.Write("  HP: [");
            Console.ForegroundColor = color;
            Console.Write(new string('█', Math.Max(0, filled)));
            Console.ResetColor();
            Console.Write(new string('░', Math.Max(0, barWidth - filled)));
            Console.WriteLine($"] {current}/{max}");
        }
    }
}
