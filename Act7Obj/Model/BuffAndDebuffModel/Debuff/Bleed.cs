using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Debuff
{
    public class Bleed : StatusEffectModel
    {
        public Bleed() 
        { 
            Name = "Bleed";
            SkillEffectDescription = "The target takes damage equal to 5% of their max health at the end of each turn for 3 turns.";
            Duration = 3; // Lasts for 3 turns
            Value = 0.05; // Deals 5% of target's max health as damage each turn
            EffectType = "Debuff";
        }
    }
}
