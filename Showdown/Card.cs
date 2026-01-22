using System;

namespace Showdown;

public class Card
{
    private int _rank;
    private string _suit;

    public Card(int rank, string suit)
    {
        this.Rank = rank;
        this.Suit = suit;
    }

    public int Rank
    {
        get { return _rank; }
        private set
        {
            if (value < 1 || value > 13)
            {
                throw new ArgumentException("Rank must be between 1 and 13.");
            }
            _rank = value;
        }
    }

    public string Suit
    {
        get { return _suit; }
        private set
        {
            if (value != "Hearts" && value != "Diamonds" && value != "Clubs" && value != "Spades")
            {
                throw new ArgumentException("Suit must be Hearts, Diamonds, Clubs, or Spades.");
            }
            _suit = value;
        }
    }
}
