using System;
using System.Collections.Generic;
using System.Linq;

namespace T03.Cards
{
    class Card
    {
        private static readonly string[] validFaces = new string[]
        { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        private static readonly string[] validSuits = new string[] { "S", "H", "D", "C" };

        private string face;
        private string suit;

        public Card(string face, string suit)
        {
            Face = face;
            Suit = suit;
        }

        public string Face
        {
            get => face;

            private set
            {
                if (!validFaces.Contains(value))
                {
                    throw new ArgumentException("Invalid card!");
                }

                face = value;
            }
        }

        public string Suit
        {
            get => suit;

            private set
            {
                if (!validSuits.Contains(value))
                {
                    throw new ArgumentException("Invalid card!");
                }

                suit = value;
            }
        }

        public override string ToString()
        => $"[{Face}{GetSuitUnicode()}]";

        private char GetSuitUnicode()
        {
            char unicode = default;
            switch (Suit)
            {
                case "S": unicode = '\u2660'; break;
                case "H": unicode = '\u2665'; break;
                case "D": unicode = '\u2666'; break;
                case "C": unicode = '\u2663'; break;
            }

            return unicode;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Card> cards = new List<Card>();
            string[] deck = Console.ReadLine().Split(", ");
            foreach (var card in deck)
            {
                string[] cardInfo = card.Split();
                string face = cardInfo[0];
                string suit = cardInfo[1];

                try
                {
                    cards.Add(new Card(face, suit));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            Console.WriteLine(string.Join(' ', cards));
        }
    }
}
