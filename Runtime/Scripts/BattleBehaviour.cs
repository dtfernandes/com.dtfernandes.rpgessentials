using System;
using System.Collections.Generic;
using System.Linq;


namespace RpgEssentials.TurnBased
{
    public interface IBattleBehaviour
    {
        void StartBehaviour();
        bool UpdateBehaviour();
        void EndBehaviour();
    }

}
