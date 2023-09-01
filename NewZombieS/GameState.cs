using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Zombie_Slayer.Properties;

namespace Zombie_Slayer
{
    [Serializable]
    public class GameState
    {
        public Player PlayerData { get; set; }
        public List<ZombieAbstract> ZombiesList { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public List<Tuple<int, int>> ZombiePositions { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerScore { get; set; }
        public bool IsGamePaused { get; set; }
        public bool IsMainSoundPlaying { get; set; }

        public GameState()
        {
            ZombiesList = new List<ZombieAbstract>();
            ZombiePositions = new List<Tuple<int, int>>();
        }

        public void Save(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving game state: " + ex.Message);
            }
        }

        public static GameState Load(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (GameState)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading game state: " + ex.Message);
                return null;
            }
        }
    }
}
