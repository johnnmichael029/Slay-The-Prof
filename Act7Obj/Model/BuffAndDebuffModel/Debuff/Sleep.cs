using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Debuff
{
    public class Sleep : StatusEffectModel
    {
        public Sleep()
        {
            Name = "Sleep";
            SkillEffectDescription = "You can't play any cards for this turn.";
            Duration = 1; 
            EffectType = "Debuff";
        }
    }
}
