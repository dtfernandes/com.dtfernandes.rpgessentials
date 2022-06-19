using System.Collections;
using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    public interface IBattleMove
    {
        void ResolveMove(BattleEntity attacker, IEnumerable<BattleEntity> target);
    }

}
