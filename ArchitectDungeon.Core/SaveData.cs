using System.Collections.Generic;

namespace DungeonArchitect
{
    public class SaveData
    {
        public List<Hero> Heroes { get; set; } = new List<Hero>();
        public List<Wall> Walls { get; set; } = new List<Wall>();
        public List<Trap> Traps { get; set; } = new List<Trap>();
        public List<TreasureChest> Chests { get; set; } = new List<TreasureChest>();
    }
}
