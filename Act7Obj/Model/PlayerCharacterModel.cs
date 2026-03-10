using Slay_The_Prof.Model.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Act7Obj.Model
{
    public class Player : PlayerCharacterModel
    {
        public required string PlayerName { get; set; }
        public int PlayerID { get; set; }
        public PlayerCharacterModel? SelectedHero { get; set; }

        public int CurrentStage { get; set; }
        public int ClassBattle { get; set; }
        public int ClassBreak { get; set; }
        public int PlayerEnergy { get; set; } = 3;

        public List<ItemModel> ItemModel { get; set; } = [];
    }
}
