using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Slayer
{
    [Serializable]
    public class GameState
    {
        public Player PlayerData { get; set; }
        public List<ZombieAbstract> ZombiesList { get; set; }
        // Add any other data you want to save/load here
    }

}
