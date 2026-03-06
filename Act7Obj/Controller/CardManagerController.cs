using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class CardManagerController
    {
        public List<CardModel> Hand = new List<CardModel>();
        public List<CardModel> DrawPile = new List<CardModel>();
        public List<CardModel> DiscardPile = new List<CardModel>();
        private Random _rng = new Random();

        public CardManagerController(List<CardModel> skillNames)
        {
            // Load your 10 skills into the draw pile
            DrawPile = new List<CardModel>(skillNames);
            Shuffle(DrawPile);
        }

        public void Shuffle(List<CardModel> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public void DrawCards(int count)
        {
            // Move current hand to discard before drawing new ones
            DiscardPile.AddRange(Hand);
            Hand.Clear();

            for (int i = 0; i < count; i++)
            {
                // If draw pile is empty, shuffle discard back in
                if (DrawPile.Count == 0)
                {
                    DrawPile.AddRange(DiscardPile);
                    DiscardPile.Clear();
                    Shuffle(DrawPile);
                }

                if (DrawPile.Count > 0)
                {
                    Hand.Add(DrawPile[0]);
                    DrawPile.RemoveAt(0);
                }
            }
        }
    }
}
