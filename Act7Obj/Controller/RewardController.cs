using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.Service;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class RewardController
    {
        public static void GiveCardReward(Player player)
        {
            // Generate the random 3 cards for this specific reward screen
            List<CardModel> currentOptions = GetRandomRewards();

            bool playerChoosingCardReward = true;
            while (playerChoosingCardReward)
            {
                Console.Clear();
                Console.WriteLine("=== VICTORY REWARD ===");
                Console.WriteLine("Choose ONE card to add to your deck:\n");

                for (int i = 0; i < currentOptions.Count; i++)
                {
                    Console.WriteLine($"    [{i + 1}] {currentOptions[i].Name.PadRight(15)} | {currentOptions[i].CardType}");
                }
                Console.WriteLine("    [4] View Detailed Descriptions");

                Console.Write("\nChoice: ");
                string input = Console.ReadLine();


                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                {
                    CardModel selected = currentOptions[choice - 1];

                    // Add to Player's lists
                    player.StartingDeck.Add(selected);
                    player.SkillNames.Add(selected.Name);
                    player.SkillDescriptions.Add(selected.CardDescription);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n>> SUCCESS: [{selected.Name}] added to your deck! <<");
                    Console.ResetColor();

                    playerChoosingCardReward = false;
                }
                else if (input == "4")
                {
                    Console.Clear();
                    Console.WriteLine("--- REWARD DETAILS ---");
                    foreach (var card in currentOptions)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"* {card.Name} (ENERGY: {card.EnergyCost}) ({card.CardType}):");
                        Console.ResetColor();
                        Console.WriteLine($"  {card.CardDescription}\n");

                    }

                    Console.Write("Press any key to go back...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid selection!");
                    Console.ReadKey();
                }
            }
        }

        public static List<CardModel> GetRandomRewards()
        {
            List<CardModel> rewardPool =
            [
                new()
                {
                    Name = "Review Me",
                    CardType = "Attack",
                    EnergyCost = 1,
                    BaseDamage = 10,
                    CardDescription = "Nag-review naman ako, pero bakit ganoon? You throw your failed test at the enemy, they take 10 damage, along with your hopes and dreams."
                },
                new()
                {
                    Name = "Final Exam",
                    CardType = "Attack",
                    Multiplier = 3.0,
                    EnergyCost = 3,
                    CardDescription = "A massive mental assault dealing 300% attack damage."
                },
                new()
                {
                    Name = "Caffeine Rush",
                    CardType = "Skill",
                    EnergyCost = 0,
                    AddedStatuses = ["Attack Boost"],
                    StatusDuration = 1,
                    CardDescription = "Gain Attack Boost for 1 turn."
                },
                  new()
                {
                    Name = "Library Session",
                    CardType = "Skill",
                    EnergyCost = 1,
                    Armor = 5,
                    DrawAmount = 2,
                    CardDescription = "Aristain gather his classmate to sleep in Library instead of learning. Now he draw 2 cards and gained 5 armor."
                },
                new()
                {
                    Name = "Asim amoy",
                    CardType = "Attack",
                    EnergyCost = 1,
                    AddedStatuses = ["Poison"],
                    StatusDuration = 3,
                    CardDescription = "Violeta releases a stench so bad that he will attack enemey and apply Poison for 3 turns"
                }
            ];
            // 1. Shuffle the list using Fisher-Yates algorithm
            Random rng = new Random();
            int n = rewardPool.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (rewardPool[n], rewardPool[k]) = (rewardPool[k], rewardPool[n]);
            }

            // 2. Take only the first 3 from the shuffled list
            return rewardPool.GetRange(0, 3);
        }

        public static void GetRewardsFromStrangeManAndSaveData(Player player)
        {
            player.PlayerGold += 150;
            player.Health -= 10;
            player.ClassBattle += 1;
            DatabaseService.SavePlayerData(player);

            Console.WriteLine("You accepted the stranger offer. You gained 150 gold but lost 10 health and feel your butt ache....");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Current Gold: {player.PlayerGold}\nCurrent Health: {player.Health}\nClass Battle: {player.ClassBattle}");
            Console.ResetColor();
            Console.WriteLine("Your data has been saved...");
            TextMoveInUIController.BottomRightPromptContinue();
            StagesInterfaceView.ShowStage1Battle2Interface(player);
        }
    }
}
