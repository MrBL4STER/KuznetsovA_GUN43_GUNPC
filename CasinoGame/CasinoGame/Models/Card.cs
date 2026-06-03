using CasinoGame.Models.Enums;

namespace CasinoGame.Models
{
    public readonly struct Card
    {
        public CardSuit Suit { get; }
        public CardRank Rank { get; }

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int GetValue()
        {
            int value = (int)Rank;
            return value;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}