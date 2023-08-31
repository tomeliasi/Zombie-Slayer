using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Zombie_Slayer
{
    [Serializable]
    public class GameState
    {
        public Player PlayerData { get; set; }
        public List<ZombieAbstract> ZombiesList { get; set; }

        // Add other game state data here as needed

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
                // Handle the exception, e.g., log it or show an error message
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
                // Handle the exception, e.g., log it or show an error message
                Console.WriteLine("Error loading game state: " + ex.Message);
                return null;
            }
        }
    }
}
