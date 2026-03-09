using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Buff
{
    public class AttackBoost : StatusEffectModel
    {
        public AttackBoost() 
        { 
                Name = "Attack Boost";
                SkillEffectDescription = "Increases attack damage by 20% for 3 turns.";
                Duration = 3; // Lasts for 3 turns
                Value = 0.20; // Increases attack damage by 20%
                EffectType = "Buff";
        }
    }
}
