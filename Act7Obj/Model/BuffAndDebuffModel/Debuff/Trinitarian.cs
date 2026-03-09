using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.BuffAndDebuffModel.Debuff
{
    public class Trinitarian : PassiveEffectModel
    {
        public Trinitarian()
        {
            PassiveName = "Trinitarian";
            PassiveEffectDescription = "For every 3 turns, you will take 3 Trinity Cards. Trinity messes up your hand.";
            Duration = 3; // Every 3 turns, the effect will trigger
            Value = 3; // Number of cards to add to player's hand every 3 turns
            EffectType = "Debuff";
        }
    }
}
