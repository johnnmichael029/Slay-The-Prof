using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model
{
    public class PassiveEffectModel
    {
        public string PassiveName { get; set; }
        public string PassiveEffectDescription { get; set; }
        public double Value { get; set; }
        public int  Duration { get; set; }
        public string EffectType { get; set; } // "Buff" or "Debuff"
    }
}
