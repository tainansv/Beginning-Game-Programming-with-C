using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCards;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            Card[] cards = new Card[5];

            deck.Shuffle();

            cards[0] = deck.TakeTopCard();
            cards[0].FlipOver();
            cards[1] = deck.TakeTopCard();
            cards[1].FlipOver();

            Console.WriteLine("Cards:\n\n{0} of {1}\n{2} of {3}",
                cards[0].Rank, cards[0].Suit, cards[1].Rank, cards[1].Suit);
        }
    }
}
