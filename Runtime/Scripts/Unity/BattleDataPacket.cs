using RpgEssentials.TurnBased;
using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    /// <summary>
    /// The information that describes a battle. 
    /// </summary>
    public class BattleDataPacket
    {
        // Enemies
        public IEnumerable<BattleEntity> Enemies { get; private set; }
        public IEnumerable<BattleEntity> Party { get; private set; }

        // Circumstances -- Flank, Surprise etc
        // private [some enum]

        public BattleDataPacket(IEnumerable<BattleEntity> enemies, IEnumerable<BattleEntity> party)
        {
            Enemies = enemies;
            Party = party;
        }
    }
}