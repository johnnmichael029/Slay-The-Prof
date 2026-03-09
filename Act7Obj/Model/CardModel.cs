using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model
{
    public class CardModel
    {
        public string Name { get; set; }
        public List<string> AddedStatuses { get; set; } = new List<string>();
        public int StatusDuration { get; set; }
        public int BaseDamage { get; set; }
        public double Multiplier { get; set; }
        public int EnergyCost { get; set; }
        public string CardType { get; set; } // "Attack", "Skill", "Power"
        public string CardDescription { get; set; }
        public int Armor {  get; set; }
        public int DrawAmount { get; set; }
        public int AttackCount { get; set; }
    }
}
