using Act7Obj.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Act7Obj.Model
{
    public class Sumayang : PlayerCharacterModel
    {
        public Sumayang()
        {
            CharacterName = "Sumayang";
            CharacterType = "Tank";
            CharacterDescription = "Sumayang is a resilient and steadfast character, embodying the spirit of a tank. With unwavering determination and a heart full of courage, Sumayang stands as a guardian, ready to protect allies and endure the toughest battles. His presence on the battlefield is a beacon of hope, inspiring those around him to fight with unwavering resolve.";
            Health = 200; 
            MaxHealth = 200;
            AttackDamage = 8;
            CritChance = 2;
            CritDamage = 5;
            Intelect = 5; 
            Speed = 10; 
        }
    }
}
