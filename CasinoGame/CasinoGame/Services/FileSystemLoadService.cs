using CasinoGame.Services;
using System;
using System.IO;
using System.Text.Json;

namespace CasinoGame.Services
{
    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private readonly string _basePath;

        public FileSystemSaveLoadService(string basePath)
        {
            _basePath = basePath;

            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public void SaveData(string data, string identifier)
        {
            string filePath = Path.Combine(_basePath, $"{identifier}.txt");
            File.WriteAllText(filePath, data);
        }

        public string LoadData(string identifier)
        {
            string filePath = Path.Combine(_basePath, $"{identifier}.txt");

            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }

            return null;
        }
    }
}