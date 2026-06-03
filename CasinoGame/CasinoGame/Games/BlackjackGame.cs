using CasinoGame.Core;
using CasinoGame.Models;
using CasinoGame.Models.Enums;
using System;
using System.Collections.Generic;

namespace CasinoGame.Games
{
    public class BlackjackGame : CasinoGameBase
    {
        private readonly int _numberOfCards;
        private Queue<Card> _deck;
        private List<Card> _playerCards;
        private List<Card> _computerCards;
        private readonly Random _rng = new Random();

        public BlackjackGame(int numberOfCards)
        {
            if (numberOfCards < 10 || numberOfCards > 52)
            {
                throw new ArgumentException($"Number of cards must be between 10 and 52. Got: {numberOfCards}");
            }

            _numberOfCards = numberOfCards;
            _playerCards = new List<Card>();
            _computerCards = new List<Card>();
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            List<Card> cards = new List<Card>();
            CardSuit[] suits = { CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Clubs, CardSuit.Spades };
            CardRank[] ranks = { CardRank.Six, CardRank.Seven, CardRank.Eight, CardRank.Nine,
                                 CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King, CardRank.Ace };

            for (int i = 0; i < _numberOfCards; i++)
            {
                CardSuit suit = suits[i % suits.Length];
                CardRank rank = ranks[i % ranks.Length];
                cards.Add(new Card(suit, rank));
            }

            _deck = new Queue<Card>(cards);
        }

        private void ResetAndShuffleDeck()
        {
            List<Card> cards = new List<Card>();
            CardSuit[] suits = { CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Clubs, CardSuit.Spades };
            CardRank[] ranks = { CardRank.Six, CardRank.Seven, CardRank.Eight, CardRank.Nine,
                                 CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King, CardRank.Ace };

            for (int i = 0; i < _numberOfCards; i++)
            {
                CardSuit suit = suits[i % suits.Length];
                CardRank rank = ranks[i % ranks.Length];
                cards.Add(new Card(suit, rank));
            }

            Shuffle(cards);
        }

        private void Shuffle(List<Card> cards)
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }

            _deck = new Queue<Card>(cards);
        }

        private int CalculateHandValue(List<Card> hand)
        {
            int value = 0;
            int aceCount = 0;

            foreach (Card card in hand)
            {
                int cardValue = card.GetValue();
                if (card.Rank == CardRank.Ace)
                {
                    aceCount++;
                    value += 11;
                }
                else
                {
                    value += cardValue;
                }
            }

            while (value > 21 && aceCount > 0)
            {
                value -= 10;
                aceCount--;
            }

            return value;
        }

        private void DealInitialCards()
        {
            _playerCards.Clear();
            _computerCards.Clear();

            if (_deck.Count < 4)
            {
                Console.WriteLine("Not enough cards! Reshuffling deck...");
                ResetAndShuffleDeck();
            }

            _playerCards.Add(_deck.Dequeue());
            _computerCards.Add(_deck.Dequeue());
            _playerCards.Add(_deck.Dequeue());
            _computerCards.Add(_deck.Dequeue());
        }

        private void DrawAdditionalCards(List<Card> hand)
        {
            while (CalculateHandValue(hand) < 17)
            {
                if (_deck.Count == 0)
                {
                    Console.WriteLine("Deck is empty! Reshuffling...");
                    ResetAndShuffleDeck();
                }
                hand.Add(_deck.Dequeue());
            }
        }

        private void DisplayCards()
        {
            Console.WriteLine("\n--- Your cards ---");
            foreach (Card card in _playerCards)
            {
                Console.WriteLine($"  {card}");
            }
            Console.WriteLine($"Your total: {CalculateHandValue(_playerCards)}");

            Console.WriteLine("\n--- Computer's cards ---");
            foreach (Card card in _computerCards)
            {
                Console.WriteLine($"  {card}");
            }
            Console.WriteLine($"Computer's total: {CalculateHandValue(_computerCards)}");
        }

        public override void PlayGame()
        {
            ResetAndShuffleDeck();

            DealInitialCards();
            DrawAdditionalCards(_playerCards);
            DrawAdditionalCards(_computerCards);
            DisplayCards();

            int playerValue = CalculateHandValue(_playerCards);
            int computerValue = CalculateHandValue(_computerCards);

            if (playerValue > 21 && computerValue > 21)
            {
                Console.WriteLine("\nBoth players bust! It's a draw!");
                OnDrawInvoke();
            }
            else if (playerValue > 21)
            {
                Console.WriteLine("\nYou bust! Computer wins!");
                OnLooseInvoke(playerValue, computerValue);
            }
            else if (computerValue > 21)
            {
                Console.WriteLine("\nComputer busts! You win!");
                OnWinInvoke(playerValue, computerValue);
            }
            else if (playerValue > computerValue)
            {
                Console.WriteLine("\nYou win!");
                OnWinInvoke(playerValue, computerValue);
            }
            else if (computerValue > playerValue)
            {
                Console.WriteLine("\nComputer wins!");
                OnLooseInvoke(playerValue, computerValue);
            }
            else
            {
                Console.WriteLine("\nIt's a draw! Drawing extra cards...");
                HandleDrawSituation();
            }
        }

        private void HandleDrawSituation()
        {
            int playerValue = CalculateHandValue(_playerCards);
            int computerValue = CalculateHandValue(_computerCards);
            int safetyCounter = 0;
            const int MAX_DRAW_ATTEMPTS = 20;

            while (playerValue == computerValue && playerValue < 21 && computerValue < 21 && safetyCounter < MAX_DRAW_ATTEMPTS)
            {
                if (_deck.Count < 2)
                {
                    Console.WriteLine("Not enough cards to continue draw. Game ends as draw!");
                    OnDrawInvoke();
                    return;
                }

                _playerCards.Add(_deck.Dequeue());
                _computerCards.Add(_deck.Dequeue());
                playerValue = CalculateHandValue(_playerCards);
                computerValue = CalculateHandValue(_computerCards);
                safetyCounter++;

                Console.WriteLine($"\nNew totals - You: {playerValue}, Computer: {computerValue}");
            }

            if (safetyCounter >= MAX_DRAW_ATTEMPTS)
            {
                Console.WriteLine("\nMaximum draw attempts reached! Game ends as draw!");
                OnDrawInvoke();
                return;
            }

            DisplayCards();

            if (playerValue > 21 && computerValue > 21)
            {
                Console.WriteLine("Both bust! Draw!");
                OnDrawInvoke();
            }
            else if (playerValue > 21)
            {
                Console.WriteLine("You bust! Computer wins!");
                OnLooseInvoke(playerValue, computerValue);
            }
            else if (computerValue > 21)
            {
                Console.WriteLine("Computer busts! You win!");
                OnWinInvoke(playerValue, computerValue);
            }
            else if (playerValue > computerValue)
            {
                Console.WriteLine("You win!");
                OnWinInvoke(playerValue, computerValue);
            }
            else if (computerValue > playerValue)
            {
                Console.WriteLine("Computer wins!");
                OnLooseInvoke(playerValue, computerValue);
            }
            else
            {
                Console.WriteLine("Draw!");
                OnDrawInvoke();
            }
        }
    }
}