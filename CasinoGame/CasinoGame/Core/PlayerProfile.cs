using System;

namespace CasinoGame.Core
{
    public class PlayerProfile
    {
        public string PlayerName { get; set; }
        public long Bank { get; set; }
        public DateTime LastPlayed { get; set; }

        public PlayerProfile()
        {
            PlayerName = "Unknown";
            Bank = 1000;
            LastPlayed = DateTime.Now;
        }

        public PlayerProfile(string playerName, long initialBank = 1000)
        {
            PlayerName = playerName;
            Bank = initialBank;
            LastPlayed = DateTime.Now;
        }

        public string Serialize()
        {
            return $"{PlayerName}|{Bank}|{LastPlayed:yyyy-MM-dd HH:mm:ss}";
        }

        public static PlayerProfile Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            string[] parts = data.Split('|');
            if (parts.Length != 3)
            {
                return null;
            }

            return new PlayerProfile
            {
                PlayerName = parts[0],
                Bank = long.Parse(parts[1]),
                LastPlayed = DateTime.Parse(parts[2])
            };
        }
    }
}