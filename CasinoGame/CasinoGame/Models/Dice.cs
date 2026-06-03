using CasinoGame.Exceptions;
using System;

namespace CasinoGame.Models
{
    public readonly struct Dice
    {
        private readonly int _min;
        private readonly int _max;
        private static readonly Random _random = new Random();

        public int Number { get; }

        public Dice(int min, int max)
        {
            if (min < 1)
            {
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            }
            if (max > int.MaxValue)
            {
                throw new WrongDiceNumberException(max, 1, int.MaxValue);
            }
            if (min > max)
            {
                throw new ArgumentException($"Min value {min} cannot be greater than max value {max}");
            }

            _min = min;
            _max = max;
            Number = _random.Next(_min, _max + 1);
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}