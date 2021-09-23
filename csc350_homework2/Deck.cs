using System;
using System.Collections.Generic;

namespace csc350_homework2
{
    class Deck
    {
        List<Card> cards = new List<Card>();

        public Deck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(rank.ToString(), suit.ToString()));
                }
            }
        }

        public bool Empty
        {
            get { return cards.Count == 0; }
        }

        public int Count()
        {
            return cards.Count;
        }
        public Card TakeTopCard()
        {
            if (!Empty)
            {
                Card topCard = cards[cards.Count - 1];
                cards.RemoveAt(cards.Count - 1);
                return topCard;
            }
            else
                return null;
        }

        public void Shuffle()
        {
            int swapPos;
            Card temp;
            Random rand = new Random();
            for(int i = cards.Count - 1; i > 0; i--)
            {
                swapPos = rand.Next(i);
                temp = cards[swapPos];
                cards[swapPos] = cards[i];
                cards[i] = temp;
            }
        }

        public void Cut(int cutPosition)
        {
            if (cutPosition >= cards.Count || cutPosition <= 0)
                return;

            List<Card> tempStack = new List<Card>();

            while(tempStack.Count < cutPosition)
            {
                tempStack.Add(cards[0]);
                cards.RemoveAt(0);
            }

            cards.AddRange(tempStack);
        }

        public void Print()
        {
            for(int i = 0; i < cards.Count; i++)
                Console.WriteLine("[" + i + "]" + cards[i].Rank + " of " + cards[i].Suit);
        }


    }
}