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
        public List<CardModel> StartingDeck { get; set; } = new List<CardModel>();

    }
}
