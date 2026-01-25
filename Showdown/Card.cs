using System;

namespace Showdown;

public class Card
{
    private int rank;
    private string? suit;

    public Card(int rank, string suit)
    {
        this.rank = rank;
        this.suit = suit;
    }

    public int Rank
    {
        get { return rank; }
        private set
        {
            if (value < 1 || value > 13)
            {
                throw new ArgumentException("Rank must be between 1 and 13.");
            }
            rank = value;
        }
    }

    public string Suit
    {
        get { return suit!; }
        private set
        {
            if (value != "Hearts" && value != "Diamonds" && value != "Clubs" && value != "Spades")
            {
                throw new ArgumentException("Suit must be Hearts, Diamonds, Clubs, or Spades.");
            }
            suit = value;
        }
    }

    public override string ToString()
    {
        string rankStr = Rank switch
        {
            1 => "A",
            11 => "J",
            12 => "Q",
            13 => "K",
            _ => Rank.ToString()
        };
        return $"{rankStr} of {suit}";
    }

    public int CompareTo(Card other)
    {
        // 定義 Rank 的優先度
        int GetRankPriority(int r) => r switch
        {
            1 => 13,   // A 最大
            13 => 12,  // K
            12 => 11,  // Q
            11 => 10,  // J
            _ => r - 1 // 其他牌按大小 (10->9, 9->8, ..., 2->1)
        };

        // 定義 Suit 的優先度
        int GetSuitPriority(string s) => s switch
        {
            "Spades" => 4,
            "Hearts" => 3,
            "Diamonds" => 2,
            "Clubs" => 1,
            _ => 0
        };

        int thisRankPriority = GetRankPriority(this.Rank);
        int otherRankPriority = GetRankPriority(other.Rank);

        // 先比較 Rank
        if (thisRankPriority != otherRankPriority)
        {
            return thisRankPriority.CompareTo(otherRankPriority);
        }

        // Rank 相同時，比較 Suit
        int thisSuitPriority = GetSuitPriority(this.Suit);
        int otherSuitPriority = GetSuitPriority(other.Suit);

        return thisSuitPriority.CompareTo(otherSuitPriority);
    }
}
