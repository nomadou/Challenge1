using Showdown;
using System;

// 創建玩家
List<Player> players = new List<Player>
{
    new HumanPlayer("P1"),
    new AIPlayer("P2"),
    new AIPlayer("P3"),
    new AIPlayer("P4")
};

// 創建遊戲並開始
Game game = new Game(players);
game.Start();

