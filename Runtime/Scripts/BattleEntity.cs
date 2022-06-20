using System.Collections;
using System;
using System.Linq;

namespace RpgEssentials.TurnBased
{
    public abstract class BattleEntity : IBattleEntity, IEquatable<BattleEntity>
    {
        public short InBattleID { get; set; }
        
        public int Turn { get; set; }

        public EntityMold Mold { get; set; }

        public bool IsPlayer => battleBehaviour is PlayerBattleBehaviour;

        public Action<BattleEntity> onEndTurn;

        protected IBattleBehaviour battleBehaviour;

        //Constructor for the BattleEntity class
        protected BattleEntity(IBattleBehaviour battleBehaviour)
        {
            this.battleBehaviour = battleBehaviour;
            ResetTurns();
        }


        #region Turn Functionality

        public void StartTurn()
        {
            StartOverride();
            battleBehaviour.StartBehaviour();
        }

        protected abstract void StartOverride();

        public virtual void UpdateTurn()
        {
            bool turnContinue = 
                battleBehaviour.UpdateBehaviour();

            if (!turnContinue) onEndTurn?.Invoke(this);


        }
        
        public virtual void EndTurn()
        {
            Turn--;
            battleBehaviour.EndBehaviour();
        }

        #endregion
      
        public virtual void ResetTurns()
        {
            Turn = 1;
       
        }
        public abstract float OrderFunction();
     
        public abstract BattleEntity Copy();

        public bool Equals(BattleEntity other)
        {
            return InBattleID == other.InBattleID;
        }
    }



}





