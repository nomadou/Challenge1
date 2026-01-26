namespace Showdown;

public class AIPlayer : Player
{
    private Random random = new Random();

    public AIPlayer(string defaultName) : base(defaultName) {}

    public void DisplayDefaultName()
    {
        Console.WriteLine($"AI 玩家: {Name}");
    }

    public override Card PlayCard()
    {
        if (HandCount == 0)
        {
            return null!;
        }
        
        int cardIndex = random.Next(HandCount);
        Card card = GetCard(cardIndex);
        RemoveCard(cardIndex);
        return card;
    }

    public override bool WantToExchange()
    {
        if (HasExchanged)
        {
            return false;
        }

        // AI 有 30% 的概率選擇換牌
        return random.Next(100) < 30;
    }

    public int SelectPlayerToExchange(List<Player> otherPlayers)
    {
        // AI 隨機選擇一個玩家
        return random.Next(otherPlayers.Count);
    }
}
