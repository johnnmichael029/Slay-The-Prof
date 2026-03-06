using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.BuffAndDebuffModel;
using Slay_The_Prof.Service;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class BattleController
    {
        public static void BattleControlling(Player player, Enemy enemy, CardManagerController deck)

        {
            FirstSemController.ClassBattle1(player, enemy, deck);
        }
        public static void ApplyPassiveEffect(Player player, Enemy enemy)
        {
            player.PassiveEffects.Add(new EagleEye());
            enemy.PassiveEffects.Add(new Pandekeso());
        }

        // Tbis code Apply effects for cards
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
                        Console.WriteLine($"{card.Name} applied {statusName}!");
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
                _ => null
            };
        }
        // NEW: Status Effect Processor
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

                    Console.WriteLine($"Target regenerated {regenAmount} Health!");
                }

                // 2. DECREMENT DURATION AND REMOVE AT THE VERY END
                if (currentEffect.Name != "Missed")
                {
                    if (currentEffect.Duration <= 0)
                    {
                        effects.RemoveAt(i);
                    }
                }
            }
        }

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

                    Console.WriteLine($"Target regenerated {regenAmount} Health!");
                }
            }
        }

        public static void ProcessAttackEffects(List<StatusEffectModel> effects, BaseCharacterModel target)
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
            }
        }

        public static void TickDurations(List<StatusEffectModel> effects)
        {
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                effects[i].Duration--;
                if (effects[i].Duration <= 0)
                {
                    Console.WriteLine($"{effects[i].Name} has worn off.");
                    effects.RemoveAt(i);
                }
            }
        }

        // Passive Effect Proccessor
        //public static void ProccessPassiveEffect(List<PassiveEffectModel> effects, BaseCharacterModel target)
        //{
        //    for (int i = effects.Count - 1; i >= 0; i--)
        //    {
        //        var currentEffect = effects[i];

        //        // 1. PROCESS ALL TICKING LOGIC FIRST (Damage/Heal)
        //        if (currentEffect.PassiveName == "Eagle Eye")
        //        {
        //            int bleedDmg = (int)(target.MaxHealth * currentEffect.Value);
        //            target.Health -= bleedDmg;
        //            Console.WriteLine($"Target took {bleedDmg} Bleed damage!");
        //        }

        //    }
        //}
        // NEW: Status Icon UI

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
        public static void ShowArmorStatusForPlayer(Player player)
        {
            if (player.CurrentArmor == 0) return;
            Console.WriteLine($"  ARMOR: {player.CurrentArmor}");
        }
        public static void ShowArmorStatusForEnemy(Enemy enemy)
        {
            if (enemy.CurrentArmor == 0) return;
            Console.WriteLine($"  ARMOR: {enemy.CurrentArmor}");
        }
        public static void EndBattleIfWinThenSaveProgress(Player player, Enemy enemy)
        {
            Console.Clear();
            TextMoveInUIController.CenterText($"--- {enemy.EnemyName} DEFEATED ---");

            // 1. ADD REWARDS
            player.PlayerGold += 50;
            int expGained = 100; // Base EXP for winning
            player.CurrentExp += expGained;
            TextMoveInUIController.CenterText($"Gained 50 Gold and {expGained} EXP!");

            // 2. CHECK FOR LEVEL UP
            // Formula: Level 1 needs 50, Level 2 needs 100, etc.
            CheckForLevelUp(player);

            // 4. CARD REWARD SYSTEM
            RewardController.GiveCardReward(player);

            // 5. SAVE DATA
            Slay_The_Prof.Service.DatabaseService.SavePlayerData(player);
            Console.WriteLine("\nProgress Auto-Saved. Returning to campus...");
            Console.ReadKey();
        }
        public static void CheckForLevelUp(Player player)
        {
            int expNeeded = player.PlayerLevel * 50; // Your scaling formula

            while (player.CurrentExp >= expNeeded)
            {
                player.CurrentExp -= expNeeded;
                player.PlayerLevel++;

                // 3. SCALE STATS 
                player.Health += 10; // Heal on level up
                player.AttackDamage += 2;
                player.IntelLect += 1;
                player.Speed += 1;


                Console.ForegroundColor = ConsoleColor.Green;
                TextMoveInUIController.CenterText($"LEVEL UP! You are now Level {player.PlayerLevel}!");
                TextMoveInUIController.CenterText($"Stats Increased: +10 HP, +2 ATK, +1 INT, +1 SPD");
                Console.ResetColor();

                // Recalculate for next level
                expNeeded = player.PlayerLevel * 50;
            }
        }
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

       
    }
}
