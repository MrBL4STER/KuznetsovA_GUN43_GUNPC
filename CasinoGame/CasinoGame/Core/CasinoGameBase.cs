using System;

namespace CasinoGame.Core
{
    public abstract class CasinoGameBase
    {
        public event Action<long, long> OnWin;
        public event Action<long, long> OnLoose;
        public event Action OnDraw;

        protected abstract void FactoryMethod();

        protected void OnWinInvoke(long playerScore, long computerScore)
        {
            OnWin?.Invoke(playerScore, computerScore);
        }

        protected void OnLooseInvoke(long playerScore, long computerScore)
        {
            OnLoose?.Invoke(playerScore, computerScore);
        }

        protected void OnDrawInvoke()
        {
            OnDraw?.Invoke();
        }

        public abstract void PlayGame();

        protected virtual void ValidateInputParameters() { }
    }
}