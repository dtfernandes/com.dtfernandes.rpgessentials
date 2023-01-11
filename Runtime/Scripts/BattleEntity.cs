using System;
using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    public abstract class BattleEntity : IBattleEntity, IEquatable<BattleEntity>
    {
        public short InBattleID { get; set; }

        public int Turn { get; set; }

        public EntityMold Mold { get; set; }

        public bool IsPlayer => battleBehaviour is PlayerBattleBehaviour;

        public Action<BattleEntity> onEnterTurn { get; set; }

        internal Action<BattleEntity> onEndTurn { get; set; }

        public Action<BattleEntity> onExitTurn { get; set; }

        public Action<BattleEntity> onDeath { get; set; }

        public Action<BattleEntity, IEnumerable<BattleEntity>,
            IBattleMove> onMoveUsed
        { get; set; }
        
        public bool IsDead { get; private set; }

        protected IBattleBehaviour battleBehaviour;

        //Constructor for the BattleEntity class
        protected BattleEntity(EntityMold mold, IBattleBehaviour battleBehaviour)
        {
            Mold = mold.Copy();

            int i = 0;

            if (!mold.IsPresistent)
            {
                //Generate Stats
                foreach (BattleStat stat in Mold.ToList())
                {
                    Mold.SetAtIndex(i, stat.GenerateStat());
                    i++;
                }
            }
            else
            {
                foreach (BattleStat stat in Mold.ToList())
                {
                    Mold.SetAtIndex(i, stat);
                    i++;
                }
            }

            this.battleBehaviour = battleBehaviour;

            ResetTurns();
        }


        #region Turn Functionality

        public void StartTurn()
        { 
            StartOverride();
            battleBehaviour.StartBehaviour();
            onEnterTurn?.Invoke(this);
        }

        protected abstract void StartOverride();

        public virtual void UpdateTurn()
        {
            bool turnContinue =
                battleBehaviour.UpdateBehaviour();

            //if (!turnContinue) onEndTurn?.Invoke(this);


        }

        public virtual void EndTurn()
        {
            Turn--;
            battleBehaviour.EndBehaviour();
            onExitTurn?.Invoke(this);
        }

        #endregion


        /// <summary>
        /// Method responsible for resolving a given move affecting a given list of entities.
        /// This method triggers the onMoveUsed event
        /// </summary>
        /// <param name="move">Move to be used</param>
        /// <param name="targets">Target Entities</param>
        public void UseMove(IBattleMove move, IEnumerable<BattleEntity> targets)
        {
            //Invoke event
            onMoveUsed?.Invoke(this, targets, move);

            //Resolve move
            move?.ResolveMove(this, targets);
        }

        /// <summary>
        /// Querry if the entity is dead. Update the entity accordingly.
        /// This method triggers the onDeath event
        /// </summary>
        internal void QuerryVitality()
        {
            if (!IsAlive())
            {     
                IsDead = true;
                onDeath?.Invoke(this);
            }
        }

        //Abstract method used to define if a entity is alive or not.
        protected abstract bool IsAlive();

        /// <summary>
        /// Method responsible for reseting the turns of the entity
        /// </summary>
        public virtual void ResetTurns()
        {
            Turn = 1;
        }

        /// <summary>
        /// Abstract method used to define how this entity's order is chosen.
        /// </summary>
        public abstract float OrderFunction();

        /// <summary>
        /// Method that copies the entity
        /// </summary>
        /// <returns>Copy of the entity</returns>
        public abstract BattleEntity Copy();

        /// <summary>
        /// Method that sees if two entities are equal
        /// </summary>
        /// <param name="other">Entity to compare</param>
        /// <returns>Querry result</returns>
        public bool Equals(BattleEntity other)
        {
            return InBattleID == other.InBattleID;
        }

        public void ForceEndTurn()
        {
            onEndTurn?.Invoke(this);
        }
    }
}






