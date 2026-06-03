using System;

namespace CasinoGame.Exceptions
{
    public class WrongDiceNumberException : Exception
    {
        public int InvalidNumber { get; }
        public int MinAllowed { get; }
        public int MaxAllowed { get; }

        public WrongDiceNumberException(int invalidNumber, int minAllowed, int maxAllowed)
            : base($"Invalid dice number: {invalidNumber}. Allowed range: {minAllowed} - {maxAllowed}")
        {
            InvalidNumber = invalidNumber;
            MinAllowed = minAllowed;
            MaxAllowed = maxAllowed;
        }

        public WrongDiceNumberException(string message) : base(message) { }

        public WrongDiceNumberException(string message, Exception inner) : base(message, inner) { }
    }
}