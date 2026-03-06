using Act7Obj.View;
using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Act7Obj.Model
{
    public class Aristain : PlayerCharacterModel
    {
        public Aristain()
        {
           
            CharacterName = "Aristain";
            CharacterType = "Archer";
            PlayerLevel = 1;
            PlayerGold = 100;
            CharacterDescription = "Aristain is a skilled archer with a calm demeanor. He excels at long-range combat and has a keen eye for precision. His agility allows him to dodge attacks and strike from unexpected angles.";
            Health = 80;
            MaxHealth = 80;
            AttackDamage = 15;
            CritChance = 15; 
            CritDamage = 40;
            IntelLect = 10; 
            Speed = 25;

            StartingDeck.Add(new CardModel 
            { 
                Name = "Piercing Arrow", 
                Multiplier = 1.5, 
                EnergyCost = 1, 
                CardType = "Skill",
                CardDescription = "Aristain fires a powerful arrow that pierces through enemies, dealing 150% of his Attack Damage to all enemies in a straight line. Has a 20% chance to apply a bleed effect that deals 5% of the target's max health as damage over 3 turns."
            });

            StartingDeck.Add(new CardModel 
            { 
                Name = "Sleep Mode", 
                Multiplier = 0, 
                EnergyCost = 1, 
                CardType = "Skill", 
                AddedStatuses = [ "Regen Health", "Attack Boost", "Morale" ], 
                StatusDuration = 1,
                CardDescription = "Aristain enter in sleep mode because of the boring class. After that, he wakes up with a Regen Health, Attack Boost, and Morale for 1 turn."
            });

            StartingDeck.Add(new CardModel 
            { 
                Name = "My Bestfriend", 
                Multiplier = 2.0, 
                EnergyCost = 2, 
                CardType = "Skill", 
                CardDescription = "During a strange encounter, Aristain notices Violeta is wearing the same color as him. The entire class laughs so hard it fuels his rage, causing his attack to deal 200% attack damage. [EXHAUSTED]"
            });

            StartingDeck.Add(new CardModel 
            { 
                Name = "Cough", 
                Multiplier = 0, 
                EnergyCost = 1, 
                CardType = "Skill", 
                AddedStatuses =  ["Attack Boost"], 
                StatusDuration = 3,
                CardDescription = "The class sees that Aristain gets a cough every month. Now Paul is also sick because of him! This gives Aristain a Attack Boost, dealing 20% extra damage for the next 3 attack cards he uses in this battle."
            });

            StartingDeck.Add(new CardModel 
            { 
                Name = "Sakit Tiyan Ko", 
                Multiplier = 0, 
                EnergyCost = 1, 
                CardType = "Skill", 
                AddedStatuses = ["Missed"], 
                StatusDuration = 1,
                CardDescription = "Aristain use this when he wants to go home to skip class. The enemy will not notice him, causing their next attack card type to Missed"
            });

            StartingDeck.Add(new CardModel
            {
                Name = "WashmeWep",
                BaseDamage = 5,
                EnergyCost = 1,
                CardType = "Attack",
                CardDescription = "Deals 5 damage to enemy"
            });

            StartingDeck.Add(new CardModel 
            { 
                Name = "WashMeNeyNey", 
                BaseDamage = 5, 
                EnergyCost = 1, 
                CardType = "Attack",
                CardDescription = "Deals 5 damage to enemy"
            });

            PassiveSkills.Add("Eagle Eye");
            PassiveDescriptions.Add("Aristain's keen eyesight allows him to copy code from his classmates, increasing his Attack Damage by 10%.");    
        }

    }
}
