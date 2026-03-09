using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Buff
{
    public class EagleEye : PassiveEffectModel
    {
        public EagleEye()
        {
            PassiveName = "Eagle Eye";
            PassiveEffectDescription = "Increases the damage of all attacks by 10%.";
            Value = 0.10; // Increases damage by 10%
            Duration = 999; // Passive effects can have a very long duration or be considered permanent
            EffectType = "Buff";
        }
    }
}
