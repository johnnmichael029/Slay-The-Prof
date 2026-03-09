using Act7Obj.Model;
using Act7Obj.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.CharacterModel
{
    public class Paul : PlayerCharacterModel
    {
        public Paul()
        {
            CharacterName = "Paul";
            CharacterType = "Mage";
            CharacterDescription = "Paul is a powerful mage with a deep understanding of the arcane arts. He has spent years studying ancient tomes and mastering elemental magic. With his staff in hand, Paul can unleash devastating spells that can turn the tide of battle in an instant. His calm demeanor and strategic mind make him a valuable ally in any fight.";
            Health = 70;
            MaxHealth = 70;
            AttackDamage = 25; 
            CritChance = 5;
            CritDamage = 10;
            IntelLect = 25; 
            Speed = 15; 
        }
    }
}
