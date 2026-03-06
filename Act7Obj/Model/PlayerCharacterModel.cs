using System;
using System.Collections.Generic;
using System.Text;

namespace Act7Obj.Model
{
    public class Player : PlayerCharacterModel
    {
        public required string PlayerName { get; set; }
        public int PlayerID { get; set; }
        public int CurrentArmor { get; set; } 
        public PlayerCharacterModel? SelectedHero { get; set; }

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
