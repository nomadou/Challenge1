using System;

namespace Showdown;

public class Game
{
    private List<Player> players;
    private Deck deck;
    private List<(int player1, int player2, int roundStarted)> exchanges; // 記錄所有交換信息

    public Game(List<Player> players)
    {
        this.players = players;
        this.deck = new Deck();
        this.exchanges = new List<(int, int, int)>();
    }

    public void Start()
    {
        Console.WriteLine("=== 遊戲開始 ===");
        Console.WriteLine();

        // 玩家取名
        foreach (var player in players)
        {
            if (player is HumanPlayer humanPlayer)
            {
                bool validName = false;
                while (!validName)
                {
                    validName = humanPlayer.SetNameFromInput(IsNameUnique);
                }
            }
            else if (player is AIPlayer aiPlayer)
            {
                aiPlayer.DisplayDefaultName();
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== 玩家已就位 ===");
        foreach (var player in players)
        {
            Console.WriteLine($"玩家名稱: {player.Name}");
        }

        Console.WriteLine();

        // 洗牌
        Console.WriteLine("正在洗牌...");
        deck.Shuffle();
        Console.WriteLine("洗牌完成！");

        Console.WriteLine();

        // 發牌
        Console.WriteLine("正在發牌...");
        DealCards();
        Console.WriteLine("發牌完成！");
        Console.WriteLine();

        // 顯示每個玩家的牌數
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Name} 有 {player.HandCount} 張牌");
        }

        Console.WriteLine();
        Console.WriteLine("=== 遊戲開始 ===");
        
        // 進行 13 輪遊戲
        int[] scores = new int[players.Count];
        for (int round = 1; round <= 13; round++)
        {
            Console.WriteLine();
            Console.WriteLine($"--- 第 {round} 輪 ---");
            
            // 檢查是否有交換需要在此輪結束後換回
            CheckAndRestoreExchanges(round);
            
            // 處理換牌
            HandleExchanges(round);
            
            PlayRound(scores, round);
        }

        Console.WriteLine();
        Console.WriteLine("=== 遊戲結束 ===");
        DisplayFinalScores(scores);
    }

    private void CheckAndRestoreExchanges(int currentRound)
    {
        // 檢查是否有交換需要在此輪結束後換回（3輪後）
        var exchangesToRestore = new List<(int, int, int)>();
        foreach (var exchange in exchanges)
        {
            if (exchange.Item3 + 3 == currentRound)
            {
                exchangesToRestore.Add(exchange);
            }
        }
        
        foreach (var (player1, player2, roundStarted) in exchangesToRestore)
        {
            Console.WriteLine();
            Console.WriteLine($"💫 {players[player1].Name} 和 {players[player2].Name} 的手牌換回來了！");
            players[player1].ExchangeHands(players[player2]);
            exchanges.Remove((player1, player2, roundStarted));
        }
    }

    private void HandleExchanges(int currentRound = 0)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].WantToExchange())
            {
                var otherPlayers = new List<Player>();
                var otherIndices = new List<int>();
                
                for (int j = 0; j < players.Count; j++)
                {
                    if (i != j)
                    {
                        otherPlayers.Add(players[j]);
                        otherIndices.Add(j);
                    }
                }

                int selectedIndex = -1;
                
                if (players[i] is HumanPlayer humanPlayer)
                {
                    selectedIndex = humanPlayer.SelectPlayerToExchange(otherPlayers);
                }
                else if (players[i] is AIPlayer aiPlayer)
                {
                    selectedIndex = aiPlayer.SelectPlayerToExchange(otherPlayers);
                }

                int targetPlayerIndex = otherIndices[selectedIndex];
                
                Console.WriteLine($"✨ {players[i].Name} 和 {players[targetPlayerIndex].Name} 交換手牌了！");
                players[i].ExchangeHands(players[targetPlayerIndex]);
                
                players[i].HasExchanged = true;
                players[targetPlayerIndex].HasExchanged = true;
                
                // 記錄交換
                int minIndex = Math.Min(i, targetPlayerIndex);
                int maxIndex = Math.Max(i, targetPlayerIndex);
                exchanges.Add((minIndex, maxIndex, currentRound)); // 記錄當前回合
            }
        }
    }

    private void PlayRound(int[] scores, int currentRound)
    {
        List<(int playerIndex, Card card)> playedCards = new List<(int, Card)>();

        // 所有玩家輪流出牌
        for (int i = 0; i < players.Count; i++)
        {
            Card card = players[i].PlayCard();
            if (card != null)
            {
                playedCards.Add((i, card));
            }
        }

        // 顯示出牌結果
        Console.WriteLine();
        Console.WriteLine("出牌結果：");
        foreach (var (playerIndex, card) in playedCards)
        {
            Console.WriteLine($"  {players[playerIndex].Name}: {card}");
        }

        // 比較牌的大小，找出最大的牌
        if (playedCards.Count == 0)
        {
            Console.WriteLine("  （沒有人出牌）");
            return;
        }

        int winnerIndex = 0;
        for (int i = 1; i < playedCards.Count; i++)
        {
            if (playedCards[i].card.CompareTo(playedCards[winnerIndex].card) > 0)
            {
                winnerIndex = i;
            }
        }

        // 贏家得分
        int winner = playedCards[winnerIndex].playerIndex;
        scores[winner]++;
        Console.WriteLine();
        Console.WriteLine($"🎉 {players[winner].Name} 贏得這一輪！ (得分: {scores[winner]})");
    }

    private void DisplayFinalScores(int[] scores)
    {
        for (int i = 0; i < players.Count; i++)
        {
            Console.WriteLine($"{players[i].Name}: {scores[i]} 分");
        }

        // 找出最終贏家
        int maxScore = scores.Max();
        var winners = new List<string>();
        for (int i = 0; i < players.Count; i++)
        {
            if (scores[i] == maxScore)
            {
                winners.Add(players[i].Name);
            }
        }

        if (winners.Count == 1)
        {
            Console.WriteLine();
            Console.WriteLine($"🏆 {winners[0]} 是最終贏家！");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"🏆 平手！贏家為：{string.Join(", ", winners)}");
        }
    }

    private bool IsNameUnique(string name)
    {
        return !players.Any(p => p.Name == name);
    }

    private void DealCards()
    {
        int playerIndex = 0;

        while (deck.CardCount > 0)
        {
            Card newCard = deck.DrawCard();
            players[playerIndex].AddCard(newCard);

            // 輪流發給下一個玩家
            playerIndex = (playerIndex + 1) % players.Count;
        }
    }
}
