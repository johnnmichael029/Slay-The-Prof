using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.EnemyModel
{
    public class TrinityCharacterModel : Enemy
    {
        public TrinityCharacterModel()
        {
            EnemyName = "Trinity";
            EnemyDescription = "Sa sobrang boring magturo aantukin ka.";
            EnemyLevel = "5";
            EnemyType = "Puro Reporting";
            Health = 120;
            MaxHealth = 120;
            AttackDamage = 17;
            CritChance = 20;
            CritDamage = 20;
            IntelLect = 20;
            Speed = 20;

            StartingDeck.Add(new CardModel
            {
                Name = "Endless Reporting",
                CardType = "Skill",
                CardDescription = "Reporting NANAMAN?!! Applies Sleep for 1 turn. The player cannot play cards next turn.",
                AddedStatuses = ["Sleep"], 
                StatusDuration = 1
            });

            StartingDeck.Add(new CardModel
            {
                Name = "Gibberish Speaking",
                CardType = "Attack",
                EnergyCost = 1,
                Multiplier = .5,
                AttackCount = 2,
                CardDescription = "You can't understand what she's saying. Now Deals 50% Attack Damage twice. If both hit, shuffle a Trinity into the player's draw pile. [Unplayable Cards].",
            });

            StartingDeck.Add(new CardModel
            {
                Name = "ChatGPT",
                CardType = "Attack",
                EnergyCost = 1,
                BaseDamage = 10,
                CardDescription = "She make her test using ChatGPT that make it always the answer is B. Now Deals 10 Attack Damage.",
            });

            PassiveSkills.Add("Trinitarian");
            PassiveDescriptions.Add("For every 3 turns, you will take 1 Trinity Cards. Trinity messes up your hand.");
        }
    }
}
