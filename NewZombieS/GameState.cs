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
        public int PlayerHealth { get; set; }
        public int PlayerScore { get; set; }
        public int PlayerAmmo { get; set; }
    }
}
