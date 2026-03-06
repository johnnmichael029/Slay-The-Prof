using System;
using System.Collections.Generic;
using System.Text;

namespace Act7Obj.Model
{
    public class Player : PlayerCharacterModel
    {
        public required string PlayerName { get; set; }
        public int PlayerLevel { get; set; }
        public PlayerCharacterModel? SelectedHero { get; set; }

        public void SetPlayerName(string playerName)
        {
            this.PlayerName = playerName;
        }
    }
}
