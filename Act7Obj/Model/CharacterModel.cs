using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Act7Obj.Model
{
    public class PlayerCharacterModel : BaseCharacterModel
    {
        public string CharacterName { get; set; }
        public string CharacterType { get; set; }
        public int PlayerLevel { get; set; }
        public int CurrentExp { get; set; }
        public int PlayerGold { get; set; }
        public string CharacterDescription { get; set; }
        public List<CardModel> StartingDeck { get; set; } = new List<CardModel>();

    }
}