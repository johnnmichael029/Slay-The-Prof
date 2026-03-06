using Act7Obj.Model;
using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class CombatController
{
    private static Random _rng = new Random();

        // The method MUST return a tuple (int, bool) for your battle loop to work
        public static (int damage, bool isCrit) CalculateSkillDamage(Player player, CardModel card, double moraleMultiplier)
        {
            // GATEKEEPER: If the card isn't meant to do damage, stop here!
            if (card.Multiplier <= 0)
            {
                return (0, false);
            }

            // 1. Check for ACTIVATED boosts
            double buffMult = 1.0;
            var boost = player.ActiveEffects.Find(e => e.Name == "Attack Boost");
            if (boost != null)
            {
                buffMult += boost.Value; // Only adds if it was activated!
            }

            // 2. Apply the math in the correct order
            double totalBase = player.AttackDamage * card.Multiplier * buffMult;

            // 3. Crit check (Aristain's 15% chance)
            bool isCrit = new Random().Next(1, 101) <= (player.CritChance * moraleMultiplier);

            if (isCrit)
            {
                totalBase *= (1 + (player.CritDamage / 100.0)); // +40% for Aristain
            }

            return ((int)Math.Round(totalBase), isCrit);
        }
    }
}
