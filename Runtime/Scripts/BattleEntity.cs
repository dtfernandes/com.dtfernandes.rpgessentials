using System.Collections;
using System;
using System.Linq;

namespace RpgEssentials.TurnBase
{
    public abstract class BattleEntity : IBattleEntity, IEquatable<BattleEntity>
    {
        public int Turn { get; set; }

        public EntityMold Mold { get; set; }

        protected BattleEntity()
        {
            ResetTurns();
        }

        public abstract float OrderFunction();

        public abstract void StartTurn();

        public abstract void UpdateTurn();

        public abstract BattleEntity Copy();

        public virtual void EndTurn()
        {
            Turn--;
        }

        public virtual void ResetTurns()
        {
            Turn = 1;
        }

        public abstract bool Equals(BattleEntity other);
    }



}





