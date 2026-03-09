using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.EnemyModel
{
    public class StrangerCharacterModel : Enemy
    {
        public StrangerCharacterModel()
        {
            EnemyName = "Stranger";
            EnemyDescription = "Gay as in GAY!";
            EnemyLevel = "2";
            EnemyType = "150";
            Health = 100;
            MaxHealth = 100;
            AttackDamage = 10;
            CritChance = 10;
            CritDamage = 15;
            IntelLect = 20;
            Speed = 25;

            StartingDeck.Add(new CardModel
            {
                Name = "Shady Offer",
                CardType = "Skill",
                CardDescription = "The Stranger offers a deal. The player will have Fear for 2 turns, but the Stranger gains 10 Armor.",
                AddedStatuses = [ "Fear" ],
                StatusDuration = 2,
                Armor = 10
            });
            StartingDeck.Add(new CardModel
            {
                Name = "Confusing Catwalk",
                CardType = "Skill",
                CardDescription = "The Stranger performs a dizzying walk. add Dazzled for 2 turn.",
                AddedStatuses = ["Dazzled"],
                StatusDuration = 1
            });
            StartingDeck.Add(new CardModel
            {
                Name = "Petrang Kabayo",
                CardType = "Attack",
                BaseDamage = 20,
                CardDescription = "Put a hole in your butt and deal 20 damage.",
            });

            PassiveSkills.Add("Beshie");
            PassiveDescriptions.Add("Beshie will always have support to other LGBT Community. Now he will gain a 5 Armor et the end of his turn[Can't Stack].");

        }
    }
}
