using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class ModifiedDamageController : BaseCharacterModel
    {
        public static double DeterminedTheModifiedDamageOnCardType(CardModel card, BaseCharacterModel target)
        {
            double rawDamage = card.CardType switch
            {
                "Attack" => (card.Multiplier > 0)
                    ? target.AttackDamage * card.Multiplier
                    : target.AttackDamage + card.BaseDamage,

                "Skill" when card.BaseDamage > 0 => target.AttackDamage + card.BaseDamage,

                "Skill" when card.Multiplier > 0 => target.AttackDamage * card.Multiplier,

                _ => 0 // Default case: If it's a non-damaging Skill or unknown type
            };
            return rawDamage;
        }

        public static int GetModifiedDamage(CardModel card, BaseCharacterModel target)
        {

            // 1. Determine Raw Base
            double rawDamage = DeterminedTheModifiedDamageOnCardType(card, target);
            if (rawDamage <= 0) return 0;

            // 2. STACKING LOGIC: Sum up EVERY boost
            double totalBonus = 0;

            // Sum every "Attack Boost" in ActiveEffects (Cough + Sleep Mode)
            foreach (var effect in target.ActiveEffects)
            {
                if (effect.Name == "Attack Boost")
                {
                    totalBonus += effect.Value;
                }
            }

            // Modify the damage for passive that have attack damage boost
            foreach (var passive in target.PassiveEffects)
            {
                if (passive.PassiveName == "Eagle Eye")
                {
                    totalBonus += passive.Value;
                }
            }

            // 3. Final Calculation: 20 * (1.0 + 0.1 + 0.1 + 0.1) = 20 * 1.35 = 27
            int finalDamageForBunos = (int)(target.AttackDamage * totalBonus);

            return (int)Math.Round(rawDamage + finalDamageForBunos);
        }

        public static void TakeDamage(BaseCharacterModel target, int damage)
        {
            if (target.CurrentArmor > 0)
            {
                if (damage <= target.CurrentArmor)
                {
                    target.CurrentArmor -= damage;
                    damage = 0;
                }
                else
                {
                    damage -= target.CurrentArmor;
                    target.CurrentArmor = 0;
                }
            }

            target.Health -= damage;
            if (target.Health < 0) target.Health = 0;
        }
    }
}
