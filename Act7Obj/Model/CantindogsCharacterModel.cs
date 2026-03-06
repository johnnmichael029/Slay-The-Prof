using Act7Obj.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model
{
    public class CantindogsCharacterModel : Enemy
    {
        public CantindogsCharacterModel()
        {
                EnemyName = "Cantindogs";
                EnemyDescription = "Full Stack Dev";             
                EnemyLevel = "3";
                EnemyType = "Coder";
                Health = 80;
                MaxHealth = 80;
                AttackDamage = 10; 
                CritChance = 15;
                CritDamage = 25;
                IntelLect = 20; 
                Speed = 20; 

                StartingDeck.Add(new CardModel 
                { 
                    Name = "Notepad++", 
                    BaseDamage = 10, 
                    Multiplier = 0, 
                    CardType = "Attack",
                    CardDescription = "Force the class to write code in a basic Notepad. You lose 10 Damage due to missing a single semicolon. [EXHAUSTED]"
                });

                StartingDeck.Add(new CardModel 
                { 
                    Name = "Silent Mode", 
                    Multiplier = 0, 
                    CardType = "Skill", 
                    AddedStatuses = ["Fear"], 
                    StatusDuration = 2,
                    CardDescription = "The Professor falls into a terrifying silence due to class noise. You are struck with Fear and cannot use Attack Skills for 2 turns."
                });

                StartingDeck.Add(new CardModel 
                { 
                    Name = "For Loop", 
                    Multiplier = 1.0, 
                    CardType = "Skill", 
                    AddedStatuses = ["Bleed"], 
                    StatusDuration = 3,
                    CardDescription = "FOR LOOP IS MY FAVORITE! You are caught in an infinite loop, taking Bleed damage for every attack card type you attempt for 3 turn."
                });

                PassiveSkills.Add("Pandekeso");
                PassiveDescriptions.Add("Every class, the Professor orders pandekeso to regenerate his energy from the stress of teaching. He now restores 5% of his max health at the end of his turn.");


        }
    }
}
