using CasinoGame.Core;
using CasinoGame.Games;
using CasinoGame.Services;
using System;
using System.IO;

namespace CasinoGame.Core
{
    public interface IGame
    {
        void StartGame();
    }

    public class Casino : IGame
    {
        private const long MAX_BANK = 1_000_000_000;
        private const string SAVE_PATH = "SaveData";
        private const string PLAYER_SAVE_ID = "player_profile";

        private PlayerProfile _currentPlayer;
        private readonly BlackjackGame _blackjack;
        private readonly DiceGame _diceGame;
        private readonly FileSystemSaveLoadService _saveLoadService;

        public Casino()
        {
            _saveLoadService = new FileSystemSaveLoadService(SAVE_PATH);
            _blackjack = new BlackjackGame(36);
            _diceGame = new DiceGame(6, 1, 6); 
        }

        private void LoadOrCreateProfile()
        {
            Console.WriteLine("=== WELCOME TO THE CASINO ===\n");

            string savedData = _saveLoadService.LoadData(PLAYER_SAVE_ID);
            _currentPlayer = PlayerProfile.Deserialize(savedData);

            if (_currentPlayer == null)
            {
                Console.Write("No profile found. Enter your name: ");
                string playerName = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.Write("Name cannot be empty. Enter your name: ");
                    playerName = Console.ReadLine();
                }

                _currentPlayer = new PlayerProfile(playerName, 1000);
                Console.WriteLine($"\nWelcome {playerName}! You have received 1000 chips as a starting bonus!\n");
            }
            else
            {
                Console.WriteLine($"Welcome back, {_currentPlayer.PlayerName}!");
                Console.WriteLine($"Your current balance: {_currentPlayer.Bank} chips\n");
            }
        }

        private void SaveProfile()
        {
            _currentPlayer.LastPlayed = DateTime.Now;
            string serializedData = _currentPlayer.Serialize();
            _saveLoadService.SaveData(serializedData, PLAYER_SAVE_ID);
        }

        private void DisplayGameMenu()
        {
            Console.WriteLine("=== GAME MENU ===");
            Console.WriteLine("1. Blackjack (21)");
            Console.WriteLine("2. Dice Game");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose a game: ");
        }

        private int GetUserChoice()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            return -1;
        }

        private long GetBetAmount()
        {
            Console.WriteLine($"\nYour balance: {_currentPlayer.Bank} chips");
            Console.Write($"Enter your bet (1 - {_currentPlayer.Bank}): ");

            string input = Console.ReadLine();
            if (long.TryParse(input, out long bet) && bet > 0 && bet <= _currentPlayer.Bank)
            {
                return bet;
            }

            Console.WriteLine("Invalid bet amount!");
            return GetBetAmount();
        }

        private void PlayBlackjack(long bet)
        {
            bool eventFired = false;

            _blackjack.OnWin += (playerScore, computerScore) =>
            {
                if (eventFired) return;
                eventFired = true;

                _currentPlayer.Bank += bet;

                if (_currentPlayer.Bank > MAX_BANK)
                {
                    Console.WriteLine($"\n*** AMAZING! You've broken the bank! ***");
                    Console.WriteLine($"Your winnings exceeded {MAX_BANK} chips!");
                    Console.WriteLine("The casino is bankrupt! They build a new one...");
                    Console.WriteLine($"Your bank is reset to {MAX_BANK / 2} chips");
                    _currentPlayer.Bank = MAX_BANK / 2;
                }

                Console.WriteLine($"\n*** YOU WON {bet} CHIPS! ***");
                Console.WriteLine($"Your new balance: {_currentPlayer.Bank} chips");
                Console.WriteLine($"\nGame result - You: {playerScore}, Computer: {computerScore}");
            };

            _blackjack.OnLoose += (playerScore, computerScore) =>
            {
                if (eventFired) return;
                eventFired = true;

                _currentPlayer.Bank -= bet;
                Console.WriteLine($"\n*** YOU LOST {bet} CHIPS! ***");
                Console.WriteLine($"Your new balance: {_currentPlayer.Bank} chips");
                Console.WriteLine($"\nGame result - You: {playerScore}, Computer: {computerScore}");
            };

            _blackjack.OnDraw += () =>
            {
                if (eventFired) return;
                eventFired = true;

                Console.WriteLine("\n*** IT'S A DRAW! BET RETURNED ***");
                Console.WriteLine($"Your balance remains: {_currentPlayer.Bank} chips");
            };

            _blackjack.PlayGame();
        }

        private void PlayDiceGame(long bet)
        {
            bool eventFired = false;

            _diceGame.OnWin += (playerScore, computerScore) =>
            {
                if (eventFired) return;
                eventFired = true;

                _currentPlayer.Bank += bet;

                if (_currentPlayer.Bank > MAX_BANK)
                {
                    Console.WriteLine($"\n*** AMAZING! You've broken the bank! ***");
                    Console.WriteLine($"Your winnings exceeded {MAX_BANK} chips!");
                    Console.WriteLine("The casino is bankrupt! They build a new one...");
                    Console.WriteLine($"Your bank is reset to {MAX_BANK / 2} chips");
                    _currentPlayer.Bank = MAX_BANK / 2;
                }

                Console.WriteLine($"\n*** YOU WON {bet} CHIPS! ***");
                Console.WriteLine($"Your new balance: {_currentPlayer.Bank} chips");
                Console.WriteLine($"\nGame result - You: {playerScore}, Computer: {computerScore}");
            };

            _diceGame.OnLoose += (playerScore, computerScore) =>
            {
                if (eventFired) return;
                eventFired = true;

                _currentPlayer.Bank -= bet;
                Console.WriteLine($"\n*** YOU LOST {bet} CHIPS! ***");
                Console.WriteLine($"Your new balance: {_currentPlayer.Bank} chips");
                Console.WriteLine($"\nGame result - You: {playerScore}, Computer: {computerScore}");
            };

            _diceGame.OnDraw += () =>
            {
                if (eventFired) return;
                eventFired = true;

                Console.WriteLine("\n*** IT'S A DRAW! BET RETURNED ***");
                Console.WriteLine($"Your balance remains: {_currentPlayer.Bank} chips");
            };

            _diceGame.PlayGame();
        }

        private bool HandleBankCheck()
        {
            if (_currentPlayer.Bank <= 0)
            {
                Console.WriteLine("\n=======================================");
                Console.WriteLine("No money? Kicked!");
                Console.WriteLine("=======================================");
                return false;
            }

            if (_currentPlayer.Bank > MAX_BANK)
            {
                Console.WriteLine("\n*** WARNING: Your bank is too large! ***");
                Console.WriteLine($"You wasted half of your bank money in casino's bar");
                _currentPlayer.Bank /= 2;
                Console.WriteLine($"Your new balance: {_currentPlayer.Bank} chips\n");
            }

            return true;
        }

        private void Farewell()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine($"Thanks for playing, {_currentPlayer.PlayerName}!");
            Console.WriteLine($"Your final balance: {_currentPlayer.Bank} chips");
            Console.WriteLine("Come back soon!");
            Console.WriteLine("=======================================");
        }

        public void StartGame()
        {
            LoadOrCreateProfile();

            bool exit = false;

            while (!exit)
            {
                if (!HandleBankCheck())
                {
                    break;
                }

                DisplayGameMenu();
                int choice = GetUserChoice();

                switch (choice)
                {
                    case 1:
                        long betBlackjack = GetBetAmount();
                        PlayBlackjack(betBlackjack);
                        break;
                    case 2:
                        long betDice = GetBetAmount();
                        PlayDiceGame(betDice);
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please select 1, 2, or 0");
                        break;
                }

                if (!exit && _currentPlayer.Bank > 0)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (_currentPlayer.Bank <= 0)
                {
                    Console.WriteLine("\nPress any key to exit...");
                    Console.ReadKey();
                    break;
                }
            }

            SaveProfile();
            Farewell();
        }
    }
}