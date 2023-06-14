using System.Collections;
using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    public interface IBattleMove
    {
        string Identifier { get; }

        void ResolveMove(BattleEntity attacker, IEnumerable<BattleEntity> target, bool check);
    }

}
