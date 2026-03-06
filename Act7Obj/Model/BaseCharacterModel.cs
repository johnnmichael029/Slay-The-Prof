using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model
{
    public abstract class BaseCharacterModel
    {
        // Stats
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttackDamage { get; set; }
        public int CritChance { get; set; }
        public int CritDamage { get; set; }
        public int Speed { get; set; }
        public int Intelect { get; set; }

        // Skills
        public List<string> SkillNames { get; set; } = new List<string>();
        public List<string> SkillDescriptions { get; set; } = new List<string>();

        // Passives
        public List<string> PassiveSkills { get; set; } = new List<string>();
        public List<string> PassiveDescriptions { get; set; } = new List<string>();

        // Status Effects
        public List<StatusEffectModel> ActiveEffects { get; set; } = new List<StatusEffectModel>();
        public List<PassiveEffectModel> PassiveEffects { get; set; } = new List<PassiveEffectModel>();

        public int GetModifiedDamage(CardModel card)
        {
            double rawDamage = 0;

            // 1. Determine Raw Base
            if (card.CardType == "Attack")
                rawDamage = this.AttackDamage + card.BaseDamage;
            else if (card.CardType == "Skill")
            {
                if (card.Multiplier <= 0) return 0;
                rawDamage = this.AttackDamage * card.Multiplier;
            }

            // 2. STACKING LOGIC: Sum up EVERY boost
            double totalBonus = 0;

            // Sum every "Attack Boost" in ActiveEffects (Cough + Sleep Mode)
            foreach (var effect in this.ActiveEffects)
            {
                if (effect.Name == "Attack Boost")
                {
                    totalBonus += effect.Value;
                }
            }

            // Modify the damage for passive that have attack damage boost
            foreach (var passive in this.PassiveEffects)
            {
                if (passive.PassiveName == "Eagle Eye")
                {
                    totalBonus += passive.Value;
                }
            }

            // 3. Final Calculation: 20 * (1.0 + 0.1 + 0.1 + 0.1) = 20 * 1.35 = 27
            int finalDamageForBunos = (int)(this.AttackDamage * totalBonus);

            return (int)Math.Round(rawDamage + finalDamageForBunos);
        }

        //public int GetModifiedDamage(CardModel card)
        //{
        //    // 1. Determine the 'Raw' damage before buffs
        //    double rawDamage = 0;

        //    if (card.CardType == "Attack")
        //    {
        //        // 15 + 5 = 20
        //        rawDamage = this.AttackDamage + card.BaseDamage;
        //    }
        //    else if (card.CardType == "Skill")
        //    {
        //        if (card.Multiplier <= 0) return 0;
        //        // 15 * 2.0 = 30
        //        rawDamage = this.AttackDamage * card.Multiplier;
        //    }

        //    // 2. Determine the 'Percentage' boost (Eagle Eye, etc.)
        //    double bonusPercent = 0;

        //    var attackBoost = this.ActiveEffects.Find(e => e.Name == "Attack Boost");
        //    if (attackBoost != null) bonusPercent += attackBoost.Value;

        //    var eagleEye = this.PassiveEffects.Find(e => e.PassiveName == "Eagle Eye");
        //    if (eagleEye != null) bonusPercent += eagleEye.Value;

        //    // 3. Final Calculation: Raw * (1 + Bonus)
        //    // WashMeWep: 20 * (1 + 0.10) = 22
        //    // My Bestfriend: 30 * (1 + 0.10) = 33
        //    return (int)Math.Round(rawDamage * (1 + bonusPercent));
        //}
    }
}
