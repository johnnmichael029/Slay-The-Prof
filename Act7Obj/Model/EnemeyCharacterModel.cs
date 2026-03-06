using Act7Obj.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model
{
    public class Enemy : BaseCharacterModel
    {
        public  string EnemyName { get; set; }
        public  string EnemyDescription { get; set; }
        public  string EnemyLevel { get; set; }
        public  string EnemyType { get; set; }
        public  int CurrentArmor { get; set; }
        public List<CardModel> StartingDeck { get; set; } = new List<CardModel>();
        public void TakeDamage(int damage)
        {
            if (CurrentArmor > 0)
            {
                if (damage <= CurrentArmor)
                {
                    CurrentArmor -= damage;
                    damage = 0;
                }
                else
                {
                    damage -= CurrentArmor;
                    CurrentArmor = 0;
                }
            }

            this.Health -= damage;
            if (this.Health < 0) this.Health = 0;
        }
    }
}
