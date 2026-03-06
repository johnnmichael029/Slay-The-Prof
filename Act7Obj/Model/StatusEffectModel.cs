using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model
{
    public class StatusEffectModel
    {
        public string Name { get; set; }
        public string SkillEffectDescription { get; set; }
        public int Duration { get; set; } // Turns or Attacks remaining
        public double Value { get; set; } // The power of the buff (e.g., 0.20 for 20%)
        public string EffectType { get; set; } // "Buff" or "Debuff"
    }
}
