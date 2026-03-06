using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Service
{
    public class CardLibraryService
    {
        // This is the master list of every card in your game
        public static List<CardModel> AllCards = new List<CardModel>
        {
            
            new() { Name = "Final Exam", CardType = "Attack", EnergyCost = 3, Multiplier = 3.0, CardDescription = "Massive mental assault dealing 300% as attack damage." },
                new()
                {
                    Name = "Caffeine Rush",
                    CardType = "Skill",
                    EnergyCost = 0,
                    AddedStatuses = ["Attack Boost"],
                    StatusDuration = 1,
                    CardDescription = "Gain Attack Boost for 1 turns."
                },
                   new()
                {
                    Name = "Library Session",
                    CardType = "Skill",
                    EnergyCost = 1,
                    Armor = 5,
                    DrawAmount = 2,
                    CardDescription = "Aristain gather his classmate to sleep in Library. Now he draw 2 cards and gained 5 armor."
                },
                new()
                {
                    Name = "Group Project",
                    CardType = "Attack",
                    EnergyCost = 2,
                    CardDescription = "Deal damage based on current Energy."
                },
                new()
                {
                    Name = "All-Nighter",
                    CardType = "Skill",
                    EnergyCost = 0,
                    CardDescription = "Double your next attack damage but lose a turn."
                },
                new()
                {
                    Name = "Review Me",
                    CardType = "Skill",
                    EnergyCost = 1,
                    BaseDamage = 10,
                    CardDescription = "Nag-review naman ako, pero bakit ganoon? You throw your failed test at the enemy, they take 10 damage, along with your hopes and dreams."
                },
        };

        public static CardModel? GetCardByName(string name)
        {
            return AllCards.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
