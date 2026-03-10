using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.BuffAndDebuffModel.Buff;
using Slay_The_Prof.Model.BuffAndDebuffModel.Debuff;
using Slay_The_Prof.Model.Items;
using Slay_The_Prof.Service;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class BattleController
    {

        public static void ApplyPassiveEffect(Player player, Enemy enemy)
        {
            player.PassiveEffects.Add(new EagleEye());
            if (enemy.EnemyName == "Cantindogs")
                enemy.PassiveEffects.Add(new Pandekeso());
            if (enemy.EnemyName == "Stranger")
                enemy.PassiveEffects.Add(new Beshie());
            if (enemy.EnemyName == "Trinity")
                enemy.PassiveEffects.Add(new Trinitarian());
        }

        // This code Apply effects for cards
        public static void ApplyCardEffect(CardModel card, Player player, Enemy enemy, bool isPlayerCaster = true)
        {

            if (card.AddedStatuses != null && card.AddedStatuses.Count > 0)
            {
                foreach (var statusName in card.AddedStatuses)
                {
                    var effect = CreateEffectByName(statusName, card.StatusDuration);
                    if (effect != null)
                    {
                        if (effect.EffectType == "Debuff")
                        {
                            if (isPlayerCaster) enemy.ActiveEffects.Add(effect);
                            else player.ActiveEffects.Add(effect);
                        }
                        else // Buffs always go to the one who played the card
                        {
                            if (isPlayerCaster) player.ActiveEffects.Add(effect);
                            else enemy.ActiveEffects.Add(effect);
                        }
                        Console.ForegroundColor = effect.EffectType == "Buff" ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine($"{card.Name} applied {statusName}!");
                        Console.ResetColor();
                    }
                }
            }
            switch (card.Name)
            {
                case "Piercing Arrow":
                    if (new Random().Next(1, 101) <= 20)
                    {
                        enemy.ActiveEffects.Add(new Bleed());
                        Console.WriteLine("Enemy is Bleeding!");
                    }
                    break;
            }
        }

        // Factory method to create status effects buff or debuff by name
        public static StatusEffectModel CreateEffectByName(string name, int duration)
        {
            return name switch
            {
                "Regen Health" => new RegenHealth { Duration = duration },
                "Attack Boost" => new AttackBoost { Duration = duration },
                "Morale" => new Morale { Duration = duration },
                "Missed" => new Missed { Duration = duration },
                "Fear" => new Fear { Duration = duration },
                "Bleed" => new Bleed { Duration = duration },
                "Poison" => new Poison { Duration = duration },
                "Dazzled" => new Dazzled { Duration = duration },
                "Sleep" => new Sleep { Duration = duration },
                _ => null
            };
        }

        // This is for effects that trigger at the start of your turn (like Regen, Armor Regen)
        public static void ProcessActiveSkillEffects(List<StatusEffectModel> effects, BaseCharacterModel target)
        {
            // Always loop backwards when removing items from a list
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                var currentEffect = effects[i];

                // 1. PROCESS ALL TICKING LOGIC FIRST (Damage/Heal)
                if (currentEffect.Name == "Regen Health")
                {
                    int regenAmount = (int)(target.MaxHealth * currentEffect.Value);
                    target.Health += regenAmount;

                    // Prevent overhealing
                    if (target.Health > target.MaxHealth) target.Health = target.MaxHealth;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Target regenerated {regenAmount} Health!");
                    Console.ResetColor();
                }
            }
        }

        // Passive Effect Processor (for things that trigger on their own, not on attack)
        public static void ProcessActivePassiveEffects(List<PassiveEffectModel> effects, BaseCharacterModel target)
        {
            // Always loop backwards when removing items from a list
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                var currentEffect = effects[i];

                // 1. PROCESS ALL TICKING LOGIC FIRST (Damage/Heal)
                if (currentEffect.PassiveName == "Pandekeso")
                {
                    int regenAmount = (int)(target.MaxHealth * currentEffect.Value);
                    target.Health += regenAmount;

                    // Prevent overhealing
                    if (target.Health > target.MaxHealth) target.Health = target.MaxHealth;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Target regenerated {regenAmount} Health!");
                    Console.ResetColor();
                }
                if (currentEffect.PassiveName == "Beshie")
                {
                    // We want to trigger the armor gain only when the target's armor hits 0 or below, so we check that condition first.
                    // This way, the shield won't refresh or trigger multiple times while the armor is still above 0.
                    // But this code can cause a bug when the enemy played a card that gives them armor,
                    // then they lose all that armor in one hit, and then they gain the new 5 armor from Beshie,
                    // ffectively giving them 5 more armor than intended. To fix this, we can add a check to only
                    // trigger the Beshie effect if the target's current armor is already at 0 or below before applying the new armor.

                    // But for now this will do.
                    if (target.CurrentArmor <= 0)
                    {
                        // This is to add the new 5 armor.
                        int shieldAmount = (int)(currentEffect.Value);
                        target.CurrentArmor += shieldAmount;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Target gained {shieldAmount} Armor!");
                        Console.ResetColor();
                    }
             
                }
            }
        }

        // This is for effects that trigger when you attack (like Bleed, Poison)
        public static void ProcessAttackEffects(List<StatusEffectModel> effects, BaseCharacterModel target )
        {
            foreach (var effect in effects)
            {
                // Only trigger Bleed damage when this is called
                if (effect.Name == "Bleed")
                {
                    int bleedDmg = (int)(target.MaxHealth * effect.Value);
                    target.Health -= bleedDmg;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Target took {bleedDmg} Bleed damage from attacking!");
                    Console.ResetColor();
                    
                }
                if (effect.Name == "Poison")
                {
                    int poisonDmg = (int)(target.MaxHealth * effect.Value);
                    target.Health -= poisonDmg;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nTarget took {poisonDmg} Poison damage!");
                    Console.ResetColor();
                   
                }                                            
            }          
        }

        public static bool ProcessAttackCount(CardModel cards, Player player, Enemy enemy, CardManagerController deck)
        {
            int successfulAttack = 0;
            Random rng = new Random(); // Create one instance outside the loop

            // Store the base damage so we don't lose it
            int baseDamage = enemy.AttackDamage;

            // Loop exactly 'AttackCount' times
            for (int i = 0; i < cards.AttackCount; i++)
            {
                // 50% chance check
                if (rng.Next(1, 101) <= 50)
                {
                    int calculatedDamage = (int)(baseDamage * cards.Multiplier);
                    player.Health -= calculatedDamage;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{enemy.EnemyName} hits with {cards.Name} for {calculatedDamage} damage!");                  
                    successfulAttack++;
                    Console.ResetColor();
                }
            }
            
            string plural = successfulAttack == 1 ? "time" : "times";
            Console.WriteLine($"{enemy.EnemyName} hit you {successfulAttack} {plural}.");

            // Guarantee the Dazzle if 2 or more hits landed
            if (successfulAttack >= 2)
            {
                AddTrinityCardToDrawPile(deck);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return true;
        }

        public static void AddTrinityCardToDrawPile(CardManagerController deck)
        {
            CardModel dazzleCard = new()
            {
                Name = "Trinity",
                EnergyCost = 99,
                CardType = "Unplayable",
                CardDescription = "Messes up your hand."
            };

            deck.DrawPile.Add(dazzleCard);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(">> The Stranger's presence shuffles a Trinity into your drawpile! <<");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void AddDazzleCardToDrawPile(CardManagerController deck)
        {
             CardModel dazzleCard = new()
                {
                    Name = "Dazzled",
                    EnergyCost = 99,
                    CardType = "Unplayable",
                    CardDescription = "Messes up your hand."
                };

                deck.DrawPile.Add(dazzleCard);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">> The Stranger's presence shuffles a Dazzled into your drawpile! <<");
                Console.ResetColor();
        }

        // This handle the Dazzled debuff which messes the players hand
        public static void HandleDazzledDebuff(List<StatusEffectModel> effects, CardManagerController deck)
        {
            foreach (var effect in effects)
            {
                if (effect.Name == "Dazzled")
                {
                    AddDazzleCardToDrawPile(deck);
                }
            }
        }

        // This is for ticking down durations and removing expired effects at the end of the turn
        public static void TickDurations(List<StatusEffectModel> effects)
        {
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                effects[i].Duration--;
                if (effects[i].Duration <= 0)
                {
                    effects.RemoveAt(i);
                }
            }
        }

        // This method can be called after every action to update the status display
        public static void ShowStatusIcons(List<StatusEffectModel> effects)
        {
            if (effects.Count == 0) return;
            Console.Write("  STATUS: ");
            foreach (var effect in effects)
            {
                Console.ForegroundColor = effect.EffectType == "Buff" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($" [{effect.Name}({effect.Duration})] ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        // This method can be called after every action to update the passive status display
        public static void ShowStatusPassives(List<PassiveEffectModel> effects)
        {
            if (effects.Count == 0) return;
            Console.Write("  PASSIVE: ");
            foreach (var effect in effects)
            {
                Console.ForegroundColor = effect.EffectType == "Buff" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($"[{effect.PassiveName}] ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        // This method can be called after every action to update the armor status display for player
        public static void ShowArmorStatusForPlayer(Player player)
        {
            if (player.CurrentArmor == 0) return;
            Console.WriteLine($"  ARMOR: {player.CurrentArmor}");
        }

        // This method can be called after every action to update the armor status display for enemy
        public static void ShowArmorStatusForEnemy(Enemy enemy)
        {
            if (enemy.CurrentArmor == 0) return;         
            Console.WriteLine($"  ARMOR: {enemy.CurrentArmor}");
        }

        // This method checks if the player has enough EXP to level up, and if so, increases their level and stats accordingly. It can be called after battles or when gaining EXP.
        public static void CheckForLevelUp(Player player)
        {
            int expNeeded = player.PlayerLevel * 50; // Your scaling formula

            while (player.CurrentExp >= expNeeded)
            {
                player.CurrentExp -= expNeeded;
                player.PlayerLevel++;

                // 3. SCALE STATS             
                player.Health += 10; // Heal on level up
                if (player.Health > player.MaxHealth) player.Health = player.MaxHealth; // Prevent overheal before leveling up
                player.AttackDamage += 2;
                player.IntelLect += 1;
                player.Speed += 1;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                TextMoveInUIController.CenterText($"LEVEL UP! You are now Level {player.PlayerLevel}!");
                TextMoveInUIController.CenterText($"Stats Increased: +10 HP, +2 ATK, +1 INT, +1 SPD");
                Console.ResetColor();
                TextMoveInUIController.BottomRightPromptContinue();

                // Recalculate for next level
                expNeeded = player.PlayerLevel * 50;
            }
        }

        // This method is called when the battle ends. It checks if the player won or lost, gives rewards if they won, and saves progress. If they lost, it deletes their save data.
        public static void EndBattleIfWinThenSaveProgress(Player player, Enemy enemy)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nCongratulations! You defeated {enemy.EnemyName}!");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // 1. ADD REWARDS
            player.PlayerGold += 50;
            int expGained = 100; // Base EXP for winning
            player.CurrentExp += expGained;
            player.ClassBattle++; // Move to next class battle for the next fight
            TextMoveInUIController.CenterText($"Gained 50 Gold and {expGained} EXP!");

            // 2. CHECK FOR LEVEL UP
            // Formula: Level 1 needs 50, Level 2 needs 100, etc.
            CheckForLevelUp(player);

            // 4. CARD REWARD SYSTEM
            RewardController.GiveCardReward(player);

            // 5. SAVE DATA
            Slay_The_Prof.Service.DatabaseService.SavePlayerData(player);
            player.ActiveEffects.Clear(); // Clear all active effects after battle
            player.PassiveEffects.Clear(); // Clear all passive effects after battle
            Console.WriteLine("\nProgress Auto-Saved");
        }

        // This method is called when the battle ends. It checks if the player won or lost, gives rewards if they won, and saves progress. If they lost, it deletes their save data.
        public static void EndBattleIfLoseThenSaveProgress(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  ACADEMIC FAILURE                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("The Professor has failed you. Your progress has been wiped.");
            Slay_The_Prof.Service.DatabaseService.DeletePlayerData(player.PlayerName);

            Console.WriteLine("\nReturning to campus (Main Menu)...");
            Console.ReadKey();
        }

        // This method is called when the player uses an item during battle. It applies the item's effect to the player and updates their stats accordingly.
        public static void UseItemToPlayer(ItemModel selected, Player player)
        {
            if (selected == null || selected.ItemEffect == null) return;

            foreach(var effect in selected.ItemEffect)
            { 
                var itemEffect = CreateEffectByName(effect, selected.Duration);

                switch (effect) 
                {
                    case "Max Health":
                        player.MaxHealth += selected.EffectValue;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You used {selected.ItemName} and increases Max Health {selected.EffectValue}");
                        Console.ResetColor();
                        break;
                    case "Heal":
                        player.Health += selected.EffectValue;
                        if (player.Health > player.MaxHealth) player.Health = player.MaxHealth; // Prevent overheal
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You used {selected.ItemName} and healed for {selected.EffectValue} HP!");
                        Console.ResetColor();
                        break;
                    case "Armor":
                        player.CurrentArmor += selected.EffectValue;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You used {selected.ItemName} and gained Armor {selected.EffectValue}");
                        Console.ResetColor();
                        break;
                    case "Attack Boost":                      
                        if (selected.EffectType == "Buff")
                        {
                             player.ActiveEffects.Add(itemEffect);
                        }
                        Console.ForegroundColor = itemEffect.EffectType == "Buff" ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You used {selected.ItemName} and gained {effect}");
                        Console.ResetColor();                   
                        break;
                    case "Energy":
                        player.PlayerEnergy += selected.EffectValue;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You used {selected.ItemName} and gained {selected.EffectValue} Energy!");
                        Console.ResetColor();
                        break; 
                    case "Morale":
                        if (selected.EffectType == "Buff")
                        {
                            player.ActiveEffects.Add(itemEffect);
                        }
                        Console.ForegroundColor = itemEffect.EffectType == "Buff" ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine($"You used {selected.ItemName} and gained {effect}");
                        Console.ResetColor();
                        break;

                    case "Cleanse":
                        //var debuffCount = player.ActiveEffects.Count(e => e.EffectType == "Debuff");                       
                        player.ActiveEffects.Clear();
                        Console.WriteLine("All debuffs removed!");
                        break;
                }

            }

            //if (selected.ItemEffect == "Heal")
            //{
            //    player.Health += selected.EffectValue;
            //    if (player.Health > player.MaxHealth) player.Health = player.MaxHealth; // Prevent overheal
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine($"You used {selected.ItemName} and healed for {selected.EffectValue} HP!");
            //    Console.ResetColor();
            //}
        }
    }
}
