using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel
{
    public class Pandekeso : PassiveEffectModel
    {
        public  Pandekeso()
        {
            PassiveName = "Pandekeso";
            PassiveEffectDescription = "Restores 5% of max health.";
            Value = 0.05; // Increases damage by 10%
            Duration = 999; // Passive effects can have a very long duration or be considered permanent
            EffectType = "Buff";
        }
    }
}
