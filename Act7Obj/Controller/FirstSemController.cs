using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.BuffAndDebuffModel;
using Slay_The_Prof.Model.Items;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class FirstSemController
    { 
        public static bool CatindogsBattle1(Player player, Enemy enemy, CardManagerController deck)
        {
            BattleController.ApplyPassiveEffect(player, enemy);
            deck.DrawCards(4);
            int turnCounter = 1;

            while (player.Health > 0 && enemy.Health > 0)
            {
                ExecutePlayerTurn(player, enemy, deck, turnCounter);

                if (enemy.Health <= 0) break;

                ExecuteEnemyTurn(player, enemy, deck);

                if (player.Health <= 0)
                {
                    BattleController.EndBattleIfLoseThenSaveProgress(player);
                    return false;
                }

                turnCounter++;
                deck.DrawCards(4);
            }

            if (enemy.Health <= 0) BattleController.EndBattleIfWinThenSaveProgress(player, enemy); return true;
        }

        public static bool StrangeManBattle(Player player, Enemy enemy, CardManagerController deck)
        {
            BattleController.ApplyPassiveEffect(player, enemy);
            deck.DrawCards(4);
            int turnCounter = 1;
            while (player.Health > 0 && enemy.Health > 0)
            {
                ExecutePlayerTurn(player, enemy, deck, turnCounter);

                if (enemy.Health <= 0) break;

                ExecuteEnemyTurn(player, enemy, deck);

                if (player.Health <= 0)
                {
                    BattleController.EndBattleIfLoseThenSaveProgress(player);
                    return false;
                }
                turnCounter++;
                deck.DrawCards(4);
            }
            if (enemy.Health <= 0) BattleController.EndBattleIfWinThenSaveProgress(player, enemy); return true;
        }

        public static bool TrinityBattle2(Player player, Enemy enemy, CardManagerController deck)
        {
            // Apply passive effects for both player and enemy before the battle starts, which may include buffs, debuffs, or other status changes that affect the flow of the battle
            BattleController.ApplyPassiveEffect(player, enemy);

            // Initial card draw for the player to start the battle with a hand of cards, allowing them to strategize and plan their first moves based on the cards they have drawn
            deck.DrawCards(4);
            int turnCounter = 1;
            int drawTrinityCardIfTurnCounter = 3; 
            while (player.Health > 0 && enemy.Health > 0)
            {
                // Execute the player's turn, which includes handling player inputs, playing cards,
                // applying effects, and updating the battle status.
                // This method will also check for win/loss conditions after the player's actions are processed.


                for (int i = turnCounter; i >= drawTrinityCardIfTurnCounter; i++)
                {
                    BattleController.AddTrinityCardToDrawPile(deck);
                    if (i == drawTrinityCardIfTurnCounter) drawTrinityCardIfTurnCounter += 3;
                }

                ExecutePlayerTurn(player, enemy, deck, turnCounter);

                if (enemy.Health <= 0) break;

                // Execute the enemy's turn, which includes random card selection, processing effects, and handling restrictions.
                ExecuteEnemyTurn(player, enemy, deck);

                if (player.Health <= 0)
                {
                    BattleController.EndBattleIfLoseThenSaveProgress(player);
                    return false;
                }
                turnCounter++;
                deck.DrawCards(4);
            }
            if (enemy.Health <= 0) BattleController.EndBattleIfWinThenSaveProgress(player, enemy);
            return true;
        }

        // Add armor base on the card's armor value to player
        private static void ProvidesArmorToPlayerOrEnemy(BaseCharacterModel target, CardModel card)
        {
            if (card.Armor > 0)
            {
                target.CurrentArmor += card.Armor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\nTarget gained {card.Armor} Armor!");
                Console.ResetColor();
            }
        }

        //// Add armor base on the card's armor value to enemy
        //private static void ProvidesArmorToEnemy(Enemy enemy, CardModel card)
        //{
        //    if (card.Armor > 0)
        //    {
        //        enemy.CurrentArmor += card.Armor;
        //        Console.ForegroundColor = ConsoleColor.Cyan;
        //        Console.WriteLine($"\n{enemy.EnemyName} gained {card.Armor} Armor!");
        //        Console.ResetColor();
        //    }
        //}

        // Draw cards based on the card's draw amount after playing the card
        public static void DrawCardsIfPlayed(CardManagerController deck, CardModel card)
        {
            if (card.DrawAmount > 0)
            {
                deck.DrawCards(card.DrawAmount); // Draw the specific amount
                Console.WriteLine($"\nPlayer Draw {card.DrawAmount} cards!");
            }
        }

        // Main method to execute the player's turn, including handling inputs, playing cards, and processing effects
        private static void ExecutePlayerTurn(Player player, Enemy enemy, CardManagerController deck, int turnCounter)
        {

            int currentEnergy = 3;
            bool playerTurnActive = true;

            while (playerTurnActive)
            {
                Console.Clear();
                TurnCounterView.DisplayTurnCounter(currentEnergy, turnCounter);
                DisplayBattleStatus(player, enemy);
                DisplayHand(deck);

                Console.Write("\nAction: ");
                string input = Console.ReadLine()!.ToUpper();

                if (input == "0") 
                { 
                    playerTurnActive = false;
                    deck.ClearHandForNextTurn();
                    continue; 
                }
                if (HandleMenuInputs(input, player, enemy)) continue;

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= deck.Hand.Count)
                {
                    CardModel card = deck.Hand[choice - 1];

                    // If Player is Missed, Fear, Sleep, restrict them from playing Attack cards
                    if (IsPlayerOrEnemyRestricted(card, player.ActiveEffects)) continue;
                    
                    //Check if player have energy
                    if (currentEnergy >= card.EnergyCost)
                    {
                        // Add Armor 
                        ProvidesArmorToPlayerOrEnemy(player, card);
                
                        if (card.CardType == "Attack")
                        {
                            BattleController.ProcessAttackEffects(player.ActiveEffects, player);
                            BattleController.HandleDazzledDebuff(player.ActiveEffects, deck);
                        }

                        // Process Card Damage and Effects
                        int damageDealt = ProcessCardDamageToPlayer(card, player, enemy);

                        currentEnergy -= card.EnergyCost;
                        deck.Hand.RemoveAt(choice - 1);
                        deck.DiscardPile.Add(card);

                        // Draw Cards after removing the play card
                        DrawCardsIfPlayed(deck, card);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou played {card.Name} that deals {damageDealt} damage!");
                        Console.ResetColor();
                    }
                    else { 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOut of Energy!"); 
                        Console.ResetColor();
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    if (enemy.Health <= 0) return;
                }
               
            }
            BattleController.ProcessActiveSkillEffects(player.ActiveEffects, player);
            BattleController.TickDurations(player.ActiveEffects);
        }

        // Main method to execute the enemy's turn, including random card selection, processing effects, and handling restrictions
        private static void ExecuteEnemyTurn(Player player, Enemy enemy, CardManagerController deck)
        {
            var skillCard = enemy.StartingDeck[new Random().Next(enemy.StartingDeck.Count)];     
            Console.WriteLine($"\n--- {enemy.EnemyName}'S TURN ---");

            // Simplified Enemy AI using your new Switch logic
            if (new Random().Next(1, 101) <= 50 && enemy.StartingDeck.Count > 0)
            {
                // Add Armor if card provides armor
                ProvidesArmorToPlayerOrEnemy(enemy, skillCard);
                

                if (skillCard.CardType == "Attack")
                {

                    if (skillCard.Name == "Gibberish Speaking")
                        if (BattleController.ProcessAttackCount(skillCard, player, enemy, deck)) 
                            return;

                    if (IsPlayerOrEnemyRestricted(skillCard, enemy.ActiveEffects)) return;
                        // Process Card Damage and Effects
                    int damageDealt = ProcessCardDamageToEnemy(skillCard, player, enemy);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nEnemy played {skillCard.Name} that deals {damageDealt}!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                else
                    Console.WriteLine($"{enemy.EnemyName} uses {skillCard.Name}!");
                    BattleController.ApplyCardEffect(skillCard, player, enemy, false);
            }
            else
            {
                int damageToPlayer = enemy.AttackDamage;
                ModifiedDamageController.TakeDamage(player, damageToPlayer);
                Console.WriteLine($"{enemy.EnemyName} deals {enemy.AttackDamage} damage!");
            }

            // Process any active passive effects on the enemy at the end of their turn
            BattleController.ProcessActivePassiveEffects(enemy.PassiveEffects, enemy);

            // Process any active attack effects on the enemy at the end of their turn
            BattleController.ProcessAttackEffects(enemy.ActiveEffects, enemy);

            // Tick down durations for active effects on the enemy at the end of their turn
            BattleController.TickDurations(enemy.ActiveEffects);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Check if the player is under the effect of Fear or Missed and restrict them from playing Attack cards
        private static bool IsPlayerOrEnemyRestricted(CardModel card, List<StatusEffectModel> effects)
        {
            foreach (var effect in effects)
            {
                
                if (effect.Name == "Fear" && card.CardType == "Attack")
                {
                    Console.WriteLine("\nYou are too scared to use an Attack card!");
                    Console.ReadKey();
                    return true;
                }
                if (effect.Name == "Sleep")
                {
                    Console.WriteLine("\nYou are sleep and cannot use cards!");
                    Console.ReadKey();
                    return true;
                }
                if (effect.Name == "Missed" && card.CardType == "Attack")
                {
                    Console.WriteLine("\nTarget try to attack but miss because you skipped the Class!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    BattleController.TickDurations(effects); 
                    return true;
                }
            }         
            return false;
        }

        // Check if the enemy is under the effect of Missed and restrict them from playing Attack cards
        //private static bool IsPlayerOrEnemyHasMissed(BaseCharacterModel target, CardModel card)
        //{
        //    var missed = target.ActiveEffects.Find(e => e.Name == "Missed");
        //    if (missed != null && card.CardType == "Attack")
        //    {
        //        Console.WriteLine("\nTarget try to attack but miss because you skipped the Class!");
        //        Console.WriteLine("Press any key to continue...");
        //        Console.ReadKey();
        //        BattleController.TickDurations(target.ActiveEffects); 
        //        return true;
        //    }
        //    return false;
        //}

        // Main method to process the damage dealt by the enemy's card, including calculating critical hits, applying effects, and updating stats
        public static int ProcessCardDamageToEnemy(CardModel card, Player player, Enemy enemy)
        {
            // 1. Get Base Damage (Attack + Card Damage + Attack Boosts)
            int baseDamage = ModifiedDamageController.GetModifiedDamage(card, enemy);   
            int finalDamage = baseDamage;
            bool wasCrit = false;

            // 2. Only roll for crit if the card actually deals damage
            if (baseDamage > 0)
            {
                // Calculate Effective Crit Chance (Base 15% * Morale multiplier)
                double effectiveCritChance = enemy.CritChance;
                var moraleEffect = enemy.ActiveEffects.Find(e => e.Name == "Morale");

                if (moraleEffect != null)
                {
                    effectiveCritChance *= (1 + moraleEffect.Value);
                }

                // Roll the dice!
                if (new Random().Next(1, 101) <= effectiveCritChance)
                {
                    wasCrit = true;
                    // CritDamage is likely a percentage (e.g., 50 for +50% damage)
                    double critBonusMultiplier = 1 + (enemy.CritDamage / 100.0);
                    finalDamage = (int)Math.Round(baseDamage * critBonusMultiplier);
                }
            }

            // 3. UI Display for Critical Hits
            if (wasCrit)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n★ CRITICAL HIT! ★");
                Console.ResetColor();
            }

            // 4. Update Stats and Apply Card Effects
            ModifiedDamageController.TakeDamage(player, finalDamage);
            BattleController.ApplyCardEffect(card, player, enemy, true);

            return finalDamage;
        }

        // Main method to process the damage dealt by the player's card, including calculating critical hits, applying effects, and updating stats
        private static int ProcessCardDamageToPlayer(CardModel card, Player player, Enemy enemy)
        {
            // 1. Get Base Damage (Attack + Card Damage + Attack Boosts)
            int baseDamage = ModifiedDamageController.GetModifiedDamage(card, player);
            int finalDamage = baseDamage;
            bool wasCrit = false;

            // 2. Only roll for crit if the card actually deals damage
            if (baseDamage > 0)
            {
                // Calculate Effective Crit Chance (Base 15% * Morale multiplier)
                double effectiveCritChance = player.CritChance;
                var moraleEffect = player.ActiveEffects.Find(e => e.Name == "Morale");

                if (moraleEffect != null)
                {
                    effectiveCritChance *= (1 + moraleEffect.Value);
                }

                // Roll the dice!
                if (new Random().Next(1, 101) <= effectiveCritChance)
                {
                    wasCrit = true;
                    // CritDamage is likely a percentage (e.g., 50 for +50% damage)
                    double critBonusMultiplier = 1 + (player.CritDamage / 100.0);
                    finalDamage = (int)Math.Round(baseDamage * critBonusMultiplier);
                }
            }

            // 3. UI Display for Critical Hits
            if (wasCrit)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n★ CRITICAL HIT! ★");
                Console.ResetColor();
            }

            // 4. Update Stats and Apply Card Effects

            // Apply the calculated damage to the player, which will handle armor reduction and health deduction
            ModifiedDamageController.TakeDamage(enemy, finalDamage);

            // Apply the card's effects after dealing damage, which may include buffs, debuffs, or other status changes
            BattleController.ApplyCardEffect(card, player, enemy, true);

            return finalDamage;
        }

        // Method to display the current battle status, including health bars, passive effects, active effects, and armor status for both the player and the enemy
        private static void DisplayBattleStatus(Player player, Enemy enemy)
        {
            Console.WriteLine($"  {player.PlayerName} (LV.{player.PlayerLevel})");
            HealthBarView.DrawHealthBar(player.Health, player.MaxHealth, ConsoleColor.Green);
            BattleController.ShowStatusPassives(player.PassiveEffects);
            BattleController.ShowStatusIcons(player.ActiveEffects);
            BattleController.ShowArmorStatusForPlayer(player);

            Console.WriteLine($"\n  {enemy.EnemyName} (LV.{enemy.EnemyLevel})");
            HealthBarView.DrawHealthBar(enemy.Health, enemy.MaxHealth, ConsoleColor.Red);
            BattleController.ShowStatusPassives(enemy.PassiveEffects);
            BattleController.ShowStatusIcons(enemy.ActiveEffects);
            BattleController.ShowArmorStatusForEnemy(enemy);
            Console.WriteLine("==================================================");
        }

        // Method to display the player's hand of cards, including their names, energy costs, and available menu options for inspecting skills, passives, enemy intel, and buffs/debuffs
        private static void DisplayHand(CardManagerController deck)
        {
            Console.WriteLine("\nYOUR HAND:");
            for (int i = 0; i < deck.Hand.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {deck.Hand[i].Name} (Energy: {deck.Hand[i].EnergyCost}) ({deck.Hand[i].CardType})");
            }
            Console.Write("\n[C] Inspect Cards | [P] Inspect Passive | [E] Enemy Intel | [B] Buff and Debuff | [I] Inventory | [0] End Turn");
        }

        // Method to handle menu inputs for inspecting skills, passives, enemy intel, and buffs/debuffs, allowing the player to view detailed information about their cards, passive effects, and the enemy's stats and abilities
        private static bool HandleMenuInputs(string input, Player player, Enemy enemy)
        {

            switch (input)
            {
                case "C":
                    int pageSize = 7; // Number of skills to show at once
                    int currentPage = 0;
                    bool viewingSkills = true;

                    while (viewingSkills)
                    {
                        Console.Clear();
                        Console.WriteLine($"=== CHARACTER SKILL BOOK (Page {currentPage + 1}) ===");

                        // Calculate which skills to show for this page
                        var pageSkills = player.StartingDeck
                            .Skip(currentPage * pageSize)
                            .Take(pageSize)
                            .ToList();

                        foreach (var card in pageSkills)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"* {card.Name} (ENERGY: {card.EnergyCost}) ({card.CardType}):");
                            Console.ResetColor();
                            Console.WriteLine($"  {card.CardDescription}\n");
                        }

                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("[N] Next Page | [P] Previous Page | [B] Back to Battle");

                        var navInput = Console.ReadKey(true).Key;

                        if (navInput == ConsoleKey.N && (currentPage + 1) * pageSize < player.StartingDeck.Count)
                            currentPage++;
                        else if (navInput == ConsoleKey.P && currentPage > 0)
                            currentPage--;
                        else if (navInput == ConsoleKey.B)
                            viewingSkills = false;
                    }
                    Console.Clear(); // Final clear to ensure the battle screen is fresh
                    break;
                case "P":
                    Console.Clear();
                    Console.WriteLine("=== PASSIVE EFFECTS ===");

                    if (player.PassiveEffects.Count == 0)
                    {
                        Console.WriteLine("You have no passive effects.");
                    }
                    else
                    {
                        foreach (var passive in player.PassiveEffects)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"▸ {passive.PassiveName}({player.CharacterName})");
                            Console.ResetColor();
                            Console.WriteLine($"  {passive.PassiveEffectDescription}\n");
                        }
                        foreach (var passive in enemy.PassiveEffects)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"▸ {passive.PassiveName}({enemy.EnemyName})");
                            Console.ResetColor();
                            Console.WriteLine($"  {passive.PassiveEffectDescription}\n");
                        }
                    }
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    break;
                case "E":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"╔══════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"║            INTEL: {enemy.EnemyName.PadRight(30)}         ║");
                    Console.WriteLine($"╚══════════════════════════════════════════════════════════╝");
                    Console.ResetColor();

                    Console.WriteLine($"\nDESCRIPTION: {enemy.EnemyDescription}");
                    Console.WriteLine("\n--- ENEMY SKILL BOOK ---");


                    // Loop through enemy skills and their descriptions
                    for (int i = 0; i < enemy.StartingDeck.Count; i++)
                    {
                        var card = enemy.StartingDeck[i];
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"* {card.Name}({card.CardType})");
                        Console.ResetColor();
                        Console.WriteLine($"  {card.CardDescription}\n");
                      
                    }

                    Console.WriteLine("Press any key to return to battle...");
                    Console.ReadKey();
                    break;
                case "B":
                    break;
                case "I":

                    List<ItemModel> items = player.ItemModel;
                    bool usingInventory = true;
                    while(usingInventory)
                    {
                        Console.Clear();
                        Console.WriteLine("=== ITEM INVENTORY ===");

                        if (player.ItemModel.Count == 0)
                        { 
                            Console.WriteLine("You have no item in your inventory");
                            Console.Write("Press any key to go back...");
                            Console.ReadKey();
                            usingInventory = false;
                        }
                        else
                        {
                           for (int i = 0; i < items.Count; i++)
                            {
                                Console.WriteLine($"    [{i + 1}] {items[i].ItemName} ({items[i].Quantity})");
                            }
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine("[I] Item Description | [B] Back to Battle");
                            Console.WriteLine("Choice: ");
                            string userInput = Console.ReadLine()!.ToUpper();

                            if (int.TryParse(userInput, out int choice) && choice >= 1 && choice <= 3)
                            {

                                ItemModel selected = items[choice - 1];

                                BattleController.UseItemToPlayer(selected, player);

                                if (selected.Quantity > 1)
                                {
                                    selected.Quantity -= 1;
                                }
                                else
                                    items.Remove(selected);

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Slay_The_Prof.Service.DatabaseService.SavePlayerData(player);
                                Console.ResetColor();
                                Console.WriteLine("\nInventory is updated!");
                                TextMoveInUIController.BottomRightPromptContinue();
                            }

                            else if(userInput == "I")
                            {
                                Console.Clear();
                                Console.WriteLine("--- ITEM DETAILS ---");
                                foreach (var item in items)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"* {item.ItemName} ({item.Quantity})");
                                    Console.ResetColor();
                                    Console.WriteLine($"  {item.Description}");
                                }
                                Console.Write("Press any key to go back...");
                                Console.ReadKey();
                            }
                            else if (userInput == "B")
                            {
                                usingInventory = false;
                            }
                        }
                    }                  
                    break;
                default:
                    break;
            }          
            return false;
        }

    }
}

