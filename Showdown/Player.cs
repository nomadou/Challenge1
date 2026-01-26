using System;

namespace Showdown;

public class Player
{
    private string? name;
    private List<Card> hands;
    private bool hasExchanged;

    public Player(string name)
    {
        this.name = name;
        this.hands = new List<Card>();
        this.hasExchanged = false;
    }

    public string Name
    {
        get { return name!; }
        set { name = value; }
    }

    public bool HasExchanged
    {
        get { return hasExchanged; }
        set { hasExchanged = value; }
    }

    public void AddCard(Card card)
    {
        hands.Add(card);
    }

    public int HandCount => hands.Count;

    public void ShowHand()
    {
        Console.WriteLine($"\n{Name} 的手牌:");
        for (int i = 0; i < hands.Count; i++)
        {
            Console.WriteLine($"  [{i + 1}] {hands[i]}");
        }
    }

    public virtual Card PlayCard()
    {
        throw new NotImplementedException("子類別必須實作此方法");
    }

    public virtual bool WantToExchange()
    {
        throw new NotImplementedException("子類別必須實作此方法");
    }

    public void ExchangeHands(Player other)
    {
        List<Card> temp = hands;
        hands = other.hands;
        other.hands = temp;
    }

    protected Card GetCard(int index)
    {
        return hands[index];
    }

    protected void RemoveCard(int index)
    {
        hands.RemoveAt(index);
    }
}
