using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Buff
{
    public class Morale : StatusEffectModel
    {
        public Morale()
        {
            Name = "Morale";
            SkillEffectDescription = "Increases the character's critical hit chance for a limited number of turns.";
            Duration = 1; // Lasts for 3 turns
            Value = 0.20; // Increases crit chance by 20%
            EffectType = "Buff";
        }
    }
}
