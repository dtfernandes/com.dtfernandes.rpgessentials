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
        public IList<BattleEntity> Enemies { get; private set; }
        public IList<BattleEntity> Party { get; private set; }

        // Circumstances -- Flank, Surprise etc
        // private [some enum]

        public BattleDataPacket(IList<BattleEntity> enemies, IList<BattleEntity> party)
        {
            Enemies = enemies;
            Party = party;
        }
    }
}