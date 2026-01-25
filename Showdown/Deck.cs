using System;
using System.Collections.Generic;

namespace Showdown;

public class Deck
{
    private List<Card> cards;

    public IReadOnlyList<Card> Cards => cards;

    public int CardCount => cards.Count;

    public Deck()
    {
        cards = new List<Card>();
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

        foreach (string suit in suits)
        {
            for (int rank = 1; rank <= 13; rank++)
            {
                cards.Add(new Card(rank, suit));
            }
        }
    }

    public void Shuffle()
    {
        Random random = new Random();
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("牌庫中沒有牌了！");
        }

        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}
