namespace Showdown;

public class HumanPlayer : Player
{
    private string defaultName;

    public HumanPlayer(string defaultName) : base(defaultName) 
    {
        this.defaultName = defaultName;
    }

    public bool SetNameFromInput(Func<string, bool> isNameValid)
    {
        Console.Write($"請輸入 {defaultName} 的玩家名稱: ");
        var inputName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputName))
        {
            return false;
        }

        if (!isNameValid(inputName))
        {
            Console.WriteLine($"錯誤：名稱 '{inputName}' 已被使用，請重新輸入。");
            return false;
        }

        Name = inputName;
        return true;
    }

    public override Card PlayCard()
    {
        while (true)
        {
            ShowHand();
            Console.Write($"\n{Name}，請選擇要打第幾張牌 (1-{HandCount}): ");
            
            if (int.TryParse(Console.ReadLine(), out int cardIndex) && cardIndex >= 1 && cardIndex <= HandCount)
            {
                Card card = GetCardAt(cardIndex - 1);
                RemoveCardAt(cardIndex - 1);
                return card;
            }
            else
            {
                Console.WriteLine("輸入無效，請重新選擇。");
            }
        }
    }

    private Card GetCardAt(int index)
    {
        // 在 Player 中會新增此 protected 方法
        return GetCard(index);
    }

    private void RemoveCardAt(int index)
    {
        // 在 Player 中會新增此 protected 方法
        RemoveCard(index);
    }
}
