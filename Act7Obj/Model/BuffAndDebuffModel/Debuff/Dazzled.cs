using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Debuff
{
    public class Dazzled : StatusEffectModel
    {
        public Dazzled()
        {
            Name = "Dazzled";
            SkillEffectDescription = "If an Attack card is played, shuffle a Dazzled into the draw pile. Dazzled is an unplayable card that messes up your hand for 1 turn.";
            Duration = 1; // Lasts for 1 turn
            EffectType = "Debuff";
        }
    }
}
