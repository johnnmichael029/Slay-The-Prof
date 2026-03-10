using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Model.Items
{
    public class ItemModel
    {
        public string ItemName { get; set; }

        public string Description { get; set; }

        public List<string> ItemEffect { get; set; } = [];

        public int ItemPrice { get; set; }

        public int EffectValue { get; set; }

        public int Duration { get; set; }

        public int Quantity { get; set; }

        public string EffectType { get; set; }



    }
}
