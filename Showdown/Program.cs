using Showdown;
using System;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Deck deck = new Deck();
deck.Shuffle();

foreach (Card card in deck.cards)
{
    Console.WriteLine(card);
}

