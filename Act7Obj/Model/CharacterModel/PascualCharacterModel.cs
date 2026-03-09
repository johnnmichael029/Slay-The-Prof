using Act7Obj.Model;
using Act7Obj.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.CharacterModel
{
    public class Pascual : PlayerCharacterModel
    {
        public Pascual()
        {
            CharacterName = "Pascual";
            CharacterType = "Fighter";
            CharacterDescription = "Pascual is a formidable fighter with a strong sense of justice. He is known for his unwavering determination and resilience in battle. With his powerful strikes and defensive skills, Pascual can withstand heavy attacks while delivering devastating blows to his enemies.";
            Health = 100;
            MaxHealth = 100;
            AttackDamage = 20;
            CritChance = 3;
            CritDamage = 20;
            IntelLect = 15; 
            Speed = 20;
        }
    }
}
