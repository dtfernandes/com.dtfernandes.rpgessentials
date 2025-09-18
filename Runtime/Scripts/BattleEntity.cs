using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RpgEssentials.TurnBased
{
    public abstract class BattleEntity : IBattleEntity, IEquatable<BattleEntity>
    {

        public event Action<BattleEntity> onEnterTurn;
        internal event Action<BattleEntity> onEndTurn;
        public event Action<BattleEntity> onExitTurn;
        public event Action<BattleEntity> onDeath;
        public event Action<BattleEntity> onRevive;
        public event Action<BattleEntity, IEnumerable<BattleEntity>, IBattleMove> onMoveUsed;


        public short InBattleID { get; set; }

        public int Turn { get; set; }
        public bool InTurn { get; protected set; }

        public EntityMold Mold { get; set; }

        public bool IsPlayer => battleBehaviour is PlayerBattleBehaviour;

        public bool IsDead { get; private set; }

        public SelectionTeam Team { get; private set; }

        protected IBattleBehaviour battleBehaviour;

        //Constructor for the BattleEntity class
        protected BattleEntity(EntityMold mold, IBattleBehaviour battleBehaviour, SelectionTeam team)
        {
            Team = team;
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

        public void StartTurn(BattleBoard board)
        {

            StartOverride();
            InTurn = true;
            battleBehaviour.StartBehaviour(board);

            onEnterTurn?.Invoke(this);

        }

        protected abstract void StartOverride();

        public virtual void EndTurn()
        {
            if (!InTurn) return;

            Turn--;
            InTurn = false;
            battleBehaviour.EndBehaviour();
            onEndTurn?.Invoke(this);
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
            //Resolve move
            move?.ResolveMove(this, targets);

            //Invoke event
            onMoveUsed?.Invoke(this, targets, move);
        }

        /// <summary>
        /// Querry if the entity is dead. Update the entity accordingly.
        /// This method triggers the onDeath event
        /// </summary>
        public void QuerryVitality()
        {
            if (!IsAlive())
            {
                IsDead = true;
                onDeath?.Invoke(this);
            }
            else
            {
                if (IsDead)
                    onRevive?.Invoke(this);

                IsDead = false;
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
        /// Method responsible for handling an entity revive funcionality
        /// </summary>
        public abstract void Revive();

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
    }
}






