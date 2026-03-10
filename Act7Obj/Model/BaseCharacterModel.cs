using Slay_The_Prof.Controller;
using Slay_The_Prof.Model.Items;
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
        public int IntelLect { get; set; }
        public int CurrentArmor { get; set; }

        // Skills
        public List<string> SkillNames { get; set; } = new List<string>();
        public List<string> SkillDescriptions { get; set; } = new List<string>();

        // Passives
        public List<string> PassiveSkills { get; set; } = new List<string>();
        public List<string> PassiveDescriptions { get; set; } = new List<string>();

        // Status Effects
        public List<StatusEffectModel> ActiveEffects { get; set; } = [];
        public List<PassiveEffectModel> PassiveEffects { get; set; } = [];

        // Items




        //public int GetModifiedDamage(CardModel card)
        //{

        //    // 1. Determine Raw Base
        //    double rawDamage = ModifiedDamageController.DeterminedTheModifiedDamageOnCardType(card, this);
        //    if (rawDamage <= 0) return 0;

        //    // 2. STACKING LOGIC: Sum up EVERY boost
        //    double totalBonus = 0;

        //    // Sum every "Attack Boost" in ActiveEffects (Cough + Sleep Mode)
        //    foreach (var effect in this.ActiveEffects)
        //    {
        //        if (effect.Name == "Attack Boost")
        //        {
        //            totalBonus += effect.Value;
        //        }
        //    }

        //    // Modify the damage for passive that have attack damage boost
        //    foreach (var passive in this.PassiveEffects)
        //    {
        //        if (passive.PassiveName == "Eagle Eye")
        //        {
        //            totalBonus += passive.Value;
        //        }
        //    }

        //    // 3. Final Calculation: 20 * (1.0 + 0.1 + 0.1 + 0.1) = 20 * 1.35 = 27
        //    int finalDamageForBunos = (int)(this.AttackDamage * totalBonus);

        //    return (int)Math.Round(rawDamage + finalDamageForBunos);
        //}

    }
}
