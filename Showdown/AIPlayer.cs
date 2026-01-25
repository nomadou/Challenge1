namespace Showdown;

public class AIPlayer : Player
{
    public AIPlayer(string defaultName) : base(defaultName) {}

    public void DisplayDefaultName()
    {
        Console.WriteLine($"AI 玩家: {Name}");
    }

    public override Card PlayCard()
    {
        Random random = new Random();
        int cardIndex = random.Next(HandCount);
        Card card = GetCard(cardIndex);
        RemoveCard(cardIndex);
        return card;
    }
}
