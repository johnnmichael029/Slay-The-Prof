using Act7Obj.Model;
using Slay_The_Prof.Model;
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
                        Console.WriteLine($"* {card.Name} ({card.CardType}):");
                        Console.ResetColor();
                        Console.WriteLine($"  {card.CardDescription}\n");
                    }
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
                    CardType = "Skill",
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
                    CardDescription = "Gain Attack Boost for 1 turns."
                },
                  new()
                {
                    Name = "Library Session",
                    CardType = "Skill",
                    EnergyCost = 1,
                    Armor = 5,
                    DrawAmount = 2,
                    CardDescription = "Aristain gather his classmate to sleep in Library. Now he draw 2 cards and gained 5 armor."
                },
                new()
                {
                    Name = "Group Project",
                    CardType = "Attack",
                    EnergyCost = 2,
                    CardDescription = "Deal damage based on current Energy."
                },
                new()
                {
                    Name = "All-Nighter",
                    CardType = "Skill",
                    EnergyCost = 0,
                    CardDescription = "Double your next attack damage but lose a turn."
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
    }
}
