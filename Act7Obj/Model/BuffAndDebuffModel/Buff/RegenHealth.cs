using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Buff
{
    public class RegenHealth : StatusEffectModel
    {
        public RegenHealth()
        {
            Name = "Regen Health";
            SkillEffectDescription = "Heals the character for a 5% of their max health at the end of each turn.";
            Duration = 1; 
            Value = 0.05; // Heals 5% of max health each turn
            EffectType = "Buff";
        }
    }
}
