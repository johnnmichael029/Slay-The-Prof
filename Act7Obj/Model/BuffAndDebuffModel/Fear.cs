using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel
{
    public class Fear : StatusEffectModel
    {
        public Fear()
        {
            Name = "Fear";
            SkillEffectDescription = "Terrified by the silence! Cannot play 'Attack' type cards. for 2 turns";
            Duration = 2;
            EffectType = "Debuff"; // This tells the game it's a negative effect
        }
    }
}
