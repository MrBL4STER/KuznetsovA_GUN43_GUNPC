using CasinoGame.Core;
using CasinoGame.Models;
using System.Collections.Generic;

namespace CasinoGame.Games
{
    public class DiceGame : CasinoGameBase
    {
        private readonly int _numberOfDice;
        private readonly int _minValue;
        private readonly int _maxValue;
        private List<Dice> _playerDice;
        private List<Dice> _computerDice;

        public DiceGame(int numberOfDice, int minValue, int maxValue)
        {
            if (numberOfDice <= 0)
            {
                throw new ArgumentException($"Number of dice must be positive. Got: {numberOfDice}");
            }
            if (minValue < 1 || maxValue > int.MaxValue || minValue > maxValue)
            {
                throw new ArgumentException($"Invalid dice range. Min: {minValue}, Max: {maxValue}");
            }

            _numberOfDice = numberOfDice;
            _minValue = minValue;
            _maxValue = maxValue;
            _playerDice = new List<Dice>();
            _computerDice = new List<Dice>();
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            _playerDice.Clear();
            _computerDice.Clear();

            for (int i = 0; i < _numberOfDice; i++)
            {
                _playerDice.Add(new Dice(_minValue, _maxValue));
                _computerDice.Add(new Dice(_minValue, _maxValue));
            }
        }

        private int CalculateTotal(List<Dice> dice)
        {
            int total = 0;
            foreach (Dice die in dice)
            {
                total += die.Number;
            }
            return total;
        }

        private void DisplayResults()
        {
            Console.WriteLine("\n--- Your dice ---");
            for (int i = 0; i < _playerDice.Count; i++)
            {
                Console.WriteLine($"  Die {i + 1}: {_playerDice[i]}");
            }
            int playerTotal = CalculateTotal(_playerDice);
            Console.WriteLine($"Your total: {playerTotal}");

            Console.WriteLine("\n--- Computer's dice ---");
            for (int i = 0; i < _computerDice.Count; i++)
            {
                Console.WriteLine($"  Die {i + 1}: {_computerDice[i]}");
            }
            int computerTotal = CalculateTotal(_computerDice);
            Console.WriteLine($"Computer's total: {computerTotal}");
        }

        public override void PlayGame()
        {
            for (int i = 0; i < _playerDice.Count; i++)
            {
                _playerDice[i] = new Dice(_minValue, _maxValue);
                _computerDice[i] = new Dice(_minValue, _maxValue);
            }

            DisplayResults();

            int playerTotal = CalculateTotal(_playerDice);
            int computerTotal = CalculateTotal(_computerDice);

            if (playerTotal > computerTotal)
            {
                Console.WriteLine("\nYou win!");
                OnWinInvoke(playerTotal, computerTotal);
            }
            else if (computerTotal > playerTotal)
            {
                Console.WriteLine("\nComputer wins!");
                OnLooseInvoke(playerTotal, computerTotal);
            }
            else
            {
                Console.WriteLine("\nIt's a draw!");
                OnDrawInvoke();
            }
        }
    }
}