using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller.TraderController
{
    public class BuyItemController
    {
        public static void GiveBuyItems(Player player)
        {
            // Generate the random 3 cards for this specific reward screen
            List<ItemModel> currentItems = GetRandomItem();

            bool playerChoosingItem = true;
            while (playerChoosingItem)
            {
                Console.Clear();
                Console.WriteLine("=== BUY ITEMS ===");
                Console.WriteLine("Buy items to add to your inventory:\n");
                Console.WriteLine($"GOLD: {player.PlayerGold}");
                for (int i = 0; i < currentItems.Count; i++)
                {
                    Console.WriteLine($"    [{i + 1}] {currentItems[i].ItemName.PadRight(20)} | {currentItems[i].ItemPrice}");
                }
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("[I] View Item Description | [B] Back to Shop");

                Console.Write("\nChoice: ");
                string input = Console.ReadLine()!.ToUpper();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= currentItems.Count)
                {
                    ItemModel selected = currentItems[choice - 1];

                    if (player.PlayerGold >= selected.ItemPrice)
                    {
                        // 1. Check if the player already has this item
                        ItemModel existingItem = player.ItemModel.Find(i => i.ItemName == selected.ItemName)!;

                        if (existingItem != null)
                        {
                            // 2. If item exists, just add to the quantity
                            existingItem.Quantity += 1;
                            player.PlayerGold -= selected.ItemPrice;
                        }
                        else
                        {
                            // 3. If it's a new item, check inventory space (e.g., max 3 unique slots)
                            if (player.ItemModel.Count >= 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n>> FAILED: Inventory full! <<");
                                Console.ResetColor();
                                TextMoveInUIController.BottomRightPromptContinue();
                                continue;
                            }

                            // 4. Add new item and set initial quantity to 1
                            player.ItemModel.Add(selected);
                            player.PlayerGold -= selected.ItemPrice;
                        }

                        // Remove from shop list
                        currentItems.RemoveAt(choice - 1);

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n>> SUCCESS: [{selected.ItemName}] bought! Gold: {player.PlayerGold} <<");

                        // Save to Database
                        Slay_The_Prof.Service.DatabaseService.SavePlayerData(player);

                        Console.ResetColor();
                        TextMoveInUIController.BottomRightPromptContinue();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n>> FAILED: Not enough gold! <<");
                        Console.ResetColor();
                        TextMoveInUIController.BottomRightPromptContinue();
                    }
                }
                else if (input == "I")
                {
                    Console.Clear();
                    Console.WriteLine("--- ITEM DETAILS ---");
                    foreach (var item in currentItems)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"* {item.ItemName} (PRICE: {item.ItemPrice})");
                        Console.ResetColor();
                        Console.WriteLine($"  {item.Description}");
                 
                    }

                    Console.Write("Press any key to go back...");
                    Console.ReadKey();
                }
                else if (input == "B")
                {
                    playerChoosingItem = false;
                }
                else
                {
                    Console.WriteLine("Invalid selection!");
                    Console.ReadKey();
                }
            }
        }

        public static List<ItemModel> GetRandomItem()
        {
            List<ItemModel> itemsToday =
            [
                new()
                {
                    ItemName = "Pastil with Egg",
                    Description = "A delicious pastil with egg. Restores 20 HP. [Yamashii's Favorite]",
                    ItemEffect = ["Heal"],
                    ItemPrice = 50,
                    EffectValue = 20,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Pastil with a Twist",
                    Description = "A delicious pastil with twist. Restores 20 HP.",
                    ItemEffect = ["Heal"],
                    ItemPrice = 50,
                    EffectValue = 20,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Pandekeso",
                    Description = "A Pandesal with Keso. Restores 10 HP.",
                    ItemEffect = ["Heal"],
                    ItemPrice = 25,
                    EffectValue = 10,
                    Quantity = 1,
                },
                  new()
                {
                    ItemName = "Pastilaw",
                    Description = "A delicious pastil with Java rice. Restores 20 HP.",
                    ItemEffect = ["Heal"],
                    ItemPrice = 50,
                    EffectValue = 20,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Isaw",
                    Description = "A delicious pastil with egg. Restores 20 HP.",
                    ItemEffect = ["Heal"],
                    ItemPrice = 50,
                    EffectValue = 20,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Coke",
                    Description = "Coke everyday is not okay. Gain Attack Boost for 3 turns",
                    ItemEffect = ["Attack Boost"],
                    EffectType = "Buff",
                    EffectValue = 20,
                    ItemPrice = 38,
                    Duration = 3,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Beng Beng",
                    Description = "Who wants a Beng Beng? Any one? Gain Attack Boost for 1 turn",
                    ItemEffect = ["Attack Boost"],
                    EffectType = "Buff",
                    EffectValue = 20,
                    ItemPrice = 20,
                    Duration = 1,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Kwek Kwek",
                    Description = "Three orange-coated quail eggs. The ultimate brain fuel after a quiz. Gain Morale for 2 turns",
                    ItemEffect = ["Morale"],
                    EffectType = "Buff",
                    EffectValue = 20,
                    ItemPrice = 35,
                    Duration = 2,
                    Quantity = 1,
                },
                 new()
                {
                    ItemName = "Siomai Rice",
                    Description = "Four pieces of siomai and a mountain of rice. The budget king. Increases 10 Max Health, and Restore 10 Health.",
                    ItemEffect = ["Max Health", "Heal"],
                    ItemPrice = 100,
                    EffectValue = 10,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Turon",
                    Description = "Fried banana with a crunchy sugar coating. Hot and sweet! Restores 8 Health.",
                    ItemEffect = ["Heal"],
                    ItemPrice = 15,
                    EffectValue = 8,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Coffee",
                    Description = "The only thing keeping you awake during reporting. Removes all debuff",
                    ItemEffect = ["Cleanse"],
                    ItemPrice = 35,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Face Mask",
                    Description = "Protects you from the toxic atmosphere of the classroom. Gains 20 Armor.",
                    ItemEffect = ["Armor"],
                    ItemPrice = 50,
                    EffectValue = 20,
                    Quantity = 1,
                },
                new()
                {
                    ItemName = "Energy Drink",
                    Description = "A sugar-filled rush. Restores 1 Energy for this turn. Effective next turn.",
                    ItemEffect = ["Energy"],
                    ItemPrice = 60,
                    EffectValue = 1,
                    Quantity = 1,
                },
            ];
            // 1. Shuffle the Items using Fisher-Yates algorithm
            Random rng = new();
            int n = itemsToday.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (itemsToday[n], itemsToday[k]) = (itemsToday[k], itemsToday[n]);
            }

            // 2. Take only the first 5 from the Items list
            return itemsToday.GetRange(0, 5);
        }
    }
}
