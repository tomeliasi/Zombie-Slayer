using System;

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
