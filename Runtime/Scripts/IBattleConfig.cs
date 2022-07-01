using System.Collections;
using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    public interface IBattleConfig
    {
        IEnumerable<EntityMold> EnemyTeam { get; }
        IEnumerable<EntityMold> PlayerTeam { get; }
    }
}


