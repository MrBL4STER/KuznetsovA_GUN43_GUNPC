using CasinoGame.Core;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Casino Game - Final Task";
            Casino casino = new Casino();
            casino.StartGame();
        }
    }
}