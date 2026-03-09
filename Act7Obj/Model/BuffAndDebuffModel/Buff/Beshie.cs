using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Buff
{
    public class Beshie : PassiveEffectModel
    {
        public Beshie()
        {
            PassiveName = "Beshie";
            PassiveEffectDescription = "Gain 5 Armor at the end of enemy turn but it will not gained if it has already an armor. ";
            Value = 5; // Grants 5 Armor
            Duration = 999; // Passive effects can have a very long duration or be considered permanent
            EffectType = "Buff";
        }
    }
}
