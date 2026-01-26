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
            Console.Write($"\n{Name}，請選擇要打第幾張牌 (1-{HandCount}) 或輸入'跳過': ");
            
            string? input = Console.ReadLine();
            
            if (input?.ToLower() == "跳過")
            {
                Console.WriteLine($"{Name} 選擇跳過這一輪。");
                return null!;
            }
            
            if (int.TryParse(input, out int cardIndex) && cardIndex >= 1 && cardIndex <= HandCount)
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

    public override bool WantToExchange()
    {
        if (HasExchanged)
        {
            return false;
        }

        Console.Write($"\n{Name}，要使用換牌特權嗎？(y/n): ");
        string? response = Console.ReadLine();
        return response?.ToLower() == "y";
    }

    public int SelectPlayerToExchange(List<Player> otherPlayers)
    {
        Console.WriteLine($"\n{Name}，選擇要和哪個玩家換牌:");
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            Console.WriteLine($"  [{i + 1}] {otherPlayers[i].Name}");
        }
        Console.Write("請選擇 (輸入編號): ");
        
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= otherPlayers.Count)
            {
                return choice - 1;
            }
            Console.WriteLine("輸入無效，請重新選擇。");
        }
    }

    private Card GetCardAt(int index)
    {
        return GetCard(index);
    }

    private void RemoveCardAt(int index)
    {
        RemoveCard(index);
    }
}
