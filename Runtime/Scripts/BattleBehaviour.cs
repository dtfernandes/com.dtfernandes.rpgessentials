using System;
using System.Collections.Generic;
using System.Linq;


namespace RpgEssentials.TurnBased
{
    public interface IBattleBehaviour
    {
        void StartBehaviour(BattleBoard board);
        bool UpdateBehaviour();
        void EndBehaviour();
    }

}
