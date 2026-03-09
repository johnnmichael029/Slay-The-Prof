using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Debuff
{
    public class Missed : StatusEffectModel
    {
        public Missed()
        {
            Name = "Missed";
            SkillEffectDescription = "The next attack will miss, dealing no damage.";
            Duration = 1; // Lasts for the next attack
            Value = 0; // No damage dealt
            EffectType = "Debuff";
        }
    }
}
