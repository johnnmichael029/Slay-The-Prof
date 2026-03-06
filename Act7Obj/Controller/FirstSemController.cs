using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.BuffAndDebuffModel;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class FirstSemController
    {
        public static void ClassBattle1(Player player, Enemy enemy, CardManagerController deck)
        {
            BattleController.ApplyPassiveEffect(player, enemy);
            deck.DrawCards(4);
            int turnCounter = 1;

            while (player.Health > 0 && enemy.Health > 0)
            {
                ExecutePlayerTurn(player, enemy, deck, turnCounter);

                if (enemy.Health <= 0) break;

                ExecuteEnemyTurn(player, enemy);

                if (player.Health <= 0)
                {
                    BattleController.EndBattleIfLoseThenSaveProgress(player);
                    return;
                }

                turnCounter++;
                deck.DrawCards(4);
            }

            if (enemy.Health <= 0) BattleController.EndBattleIfWinThenSaveProgress(player, enemy);
        }

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

                if (input == "0") { playerTurnActive = false; continue; }
                if (HandleMenuInputs(input, player, enemy)) continue;

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= deck.Hand.Count)
                {
                    CardModel card = deck.Hand[choice - 1];

                    // If Player is Missed or Feared, restrict them from playing Attack cards
                    if (IsPlayerRestricted(player, card)) continue;

                    if (currentEnergy >= card.EnergyCost)
                    {
                        int damageDealt = ProcessCardPlayToPlayer(card, player, enemy);

                        if (card.CardType == "Attack")
                        {
                            BattleController.ProcessAttackEffects(player.ActiveEffects, player);
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou played {card.Name} that deals {damageDealt} damage!");
                        Console.ResetColor();
                        
                        currentEnergy -= card.EnergyCost;
                        deck.DiscardPile.Add(card);
                        deck.Hand.RemoveAt(choice - 1);
                    }
                    else { 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOut of Energy!"); 
                        Console.ResetColor();
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    if (enemy.Health <= 0) return;
                }
               
            }
            BattleController.ProcessActiveSkillEffects(player.ActiveEffects, player);
            BattleController.TickDurations(player.ActiveEffects);
        }

        private static void ExecuteEnemyTurn(Player player, Enemy enemy)
        {
            var skillCard = enemy.StartingDeck[new Random().Next(enemy.StartingDeck.Count)];
          
            Console.WriteLine($"\n--- {enemy.EnemyName}'S TURN ---");

            // If the enemy is Missed and the card type is Attack, skip their turn and remove the Missed effect
            var missed = enemy.ActiveEffects.Find(e => e.Name == "Missed");
            if (missed != null && skillCard.CardType == "Attack")
            {
                IsEnemyRestricted(enemy, skillCard);
                // Remove the Missed effect after it triggers
                BattleController.TickDurations(enemy.ActiveEffects);
            }
            else
            { 
                // Simplified Enemy AI using your new Switch logic
                if (new Random().Next(1, 101) <= 100 && enemy.StartingDeck.Count > 0)
                {
                    
                    if (skillCard.CardType == "Attack")
                    {
                        int damageDealt = ProcessCardPlayToEnemy(skillCard, player, enemy);
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
                    player.Health -= enemy.AttackDamage;
                    Console.WriteLine($"{enemy.EnemyName} deals {enemy.AttackDamage} damage!");
                }
            }

            BattleController.ProcessActivePassiveEffects(enemy.PassiveEffects, enemy);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static bool IsPlayerRestricted(Player player, CardModel card)
        {
            var fear = player.ActiveEffects.Find(e => e.Name == "Fear");
            if (fear != null && card.CardType == "Attack")
            {
                Console.WriteLine("\nYou are too scared to use an Attack card!");
                Console.ReadKey();
                return true;
            }
            return false;
        }
        private static bool IsEnemyRestricted(Enemy enemy, CardModel card)
        {
            var missed = enemy.ActiveEffects.Find(e => e.Name == "Missed");
            if (missed != null && card.CardType == "Attack")
            {
                Console.WriteLine($"{enemy.EnemyName} tries to attack but misses because you were skipped the Class!");
                return true;
            }
            return false;
        }
        public static int ProcessCardPlayToEnemy(CardModel card, Player player, Enemy enemy)
        {
            // 1. Get Base Damage (Attack + Card Damage + Attack Boosts)
            int baseDamage = enemy.GetModifiedDamage(card);
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
            player.Health -= finalDamage;
            BattleController.ApplyCardEffect(card, player, enemy, true);

            return finalDamage;
        }
        private static int ProcessCardPlayToPlayer(CardModel card, Player player, Enemy enemy)
        {
            // 1. Get Base Damage (Attack + Card Damage + Attack Boosts)
            int baseDamage = player.GetModifiedDamage(card);
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
            enemy.Health -= finalDamage;
            BattleController.ApplyCardEffect(card, player, enemy, true);

            return finalDamage;
        }
        private static void DisplayBattleStatus(Player player, Enemy enemy)
        {
            Console.WriteLine($"  {player.PlayerName} (LV.{player.PlayerLevel})");
            StagesInterfaceView.DrawHealthBar(player.Health, player.MaxHealth, ConsoleColor.Green);
            BattleController.ShowStatusPassives(player.PassiveEffects);
            BattleController.ShowStatusIcons(player.ActiveEffects);

            Console.WriteLine($"\n  {enemy.EnemyName} (LV.{enemy.EnemyLevel})");
            StagesInterfaceView.DrawHealthBar(enemy.Health, enemy.MaxHealth, ConsoleColor.Red);
            BattleController.ShowStatusPassives(enemy.PassiveEffects);
            BattleController.ShowStatusIcons(enemy.ActiveEffects);
            Console.WriteLine("==================================================");
        }
        private static void DisplayHand(CardManagerController deck)
        {
            Console.WriteLine("\nYOUR HAND:");
            for (int i = 0; i < deck.Hand.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {deck.Hand[i].Name} (Energy: {deck.Hand[i].EnergyCost})");
            }
            Console.Write("\n[I] Inspect Skills | [P] Inspect Passive | [E] Enemy Intel | [B] Buff and Debuff | [0] End Turn");
        }
        private static bool HandleMenuInputs(string input, Player player, Enemy enemy)
        {

            switch (input)
            {
                case "I":
                    Console.Clear();
                    Console.WriteLine("=== CHARACTER SKILL BOOK ===");

                    for (int i = 0; i < player.StartingDeck.Count; i++)
                    {
                        var card = player.StartingDeck[i];
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"* {card.Name}({card.CardType}):");
                        Console.ResetColor();
                        Console.WriteLine($"  {player.SkillDescriptions[i]}\n");


                    }
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
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
                        Console.WriteLine($"▸ {card.Name}({card.CardType})");
                        Console.ResetColor();

                        // Check if descriptions exist to prevent crashes
                        string desc = (enemy.SkillDescriptions != null && i < enemy.SkillDescriptions.Count)
                            ? enemy.SkillDescriptions[i]
                            : "No description available for this professor's move.";

                        Console.WriteLine($"  {desc}\n");
                    }

                    Console.WriteLine("Press any key to return to battle...");
                    Console.ReadKey();
                    break;
                case "B":
                    break;
                default:
                    break;
            }          
            return false;
        }
    }
}

