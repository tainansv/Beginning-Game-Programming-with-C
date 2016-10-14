using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    /// <summary>
    /// Implements Lab 4 functionality
    /// </summary>
    class Program
    {
        /// <summary>
        /// Implements Lab 4 functionality
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // create a new deck and print the contents of the deck
            Deck deck = new Deck();
            deck.Print();

            // shuffle the deck and print the contents of the deck
            deck.Shuffle();
            deck.Print();

            // take the top card from the deck and print the card rank and suit
            Card card = deck.TakeTopCard();
            Console.WriteLine("Top card: {0} of {1}", card.Rank, card.Suit);

            // take the top card from the deck and print the card rank and suit
            card = deck.TakeTopCard();
            Console.WriteLine("Top card: {0} of {1}", card.Rank, card.Suit);
        }
    }
}
