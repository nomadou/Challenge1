# Poker Card Comparison System

## 📌 Overview
This is a poker card comparison program developed in **C#** as a programming assignment. The system simulates the core logic of a card game, including deck initialization, shuffling, and win-loss determination based on card rankings and suit values.

## ⚙️ Core Features
* **Deck Management**: Supports standard 52-card deck initialization and randomized shuffling.
* **Ranking Logic**: Implements comparison rules for individual cards and pairs.
* **Tie-breaking Mechanism**: Handles scenarios where ranks are equal by comparing suits (Spades > Hearts > Diamonds > Clubs).
* **Object-Oriented Design**: Utilizes classes like `Card`, `Deck`, and `Player` to maintain a clean and modular architecture.

## 🃏 Game Rules
The comparison priority is as follows:
1.  **Rank Value**: $A > K > Q > J > 10 > \dots > 2$
2.  **Suit Rank**: ♠️ Spades > ❤️ Hearts > ♦️ Diamonds > ♣️ Clubs
3.  **Winning Condition**: Higher rank wins; if ranks are identical, the highest suit determines the winner.

[Image of poker hand rankings chart]

## 🛠️ Project Structure
```text
├── src/                # Source code files
│   ├── Card.cs         # Card class (Suit & Rank definitions)
│   ├── Deck.cs         # Deck logic (Shuffling & Dealing)
│   └── Game.cs         # Main logic & Comparison engine
└── README.md           # Documentation
