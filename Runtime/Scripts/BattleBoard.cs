using System.Collections.Generic;
using System;
using System.Linq;

namespace RpgEssentials.TurnBased
{
    /// <summary>
    /// Class responsible for handling the logistics of a Battle
    /// </summary>
    public abstract class BattleBoard : IBattleBoard
    {
        // ## Events 
        public event Action<BattleEntity> onStartTurn;
        public event Action<BattleEntity> onEndTurn;

        // ## Properties
        public int Turn { get; private set; }

        protected BattleEntity turnEntity { get; set; }
        protected IList<BattleEntity> entities { get; set; }

        public BattleEntity TurnEntity
        { get => turnEntity; set => turnEntity = value; }

        public IEnumerable<BattleEntity> Entities { get => entities; }




        public BattleBoard()
        {
            entities = new List<BattleEntity> { };
        }

        /// <summary>
        /// Method responsible for add a new list of entities to 
        /// the internal list of entities.
        /// </summary>
        /// <param name="newEntities">List of entities to add.</param>
        public void AddEntities(IEnumerable<BattleEntity> newEntities)
        {
            short firstId = (short)entities.Count;
            foreach (BattleEntity entity in newEntities)
            {
                entity.InBattleID = firstId;
                firstId++;
                entities.Add(entity);
            }
        }

        public BattleEntity GetEntityFromId(int id)
        {
            return entities.First(x => x.InBattleID == id);
        }

        public void StartBattle()
        {
            //SetupBoard
            NextTurn();
        }

        public void NextTurn()
        {
            Turn++;

            // Check win condition (for now lets just check if there's only one team left)
            if (IsBattleOver())
            {

                return;
            }


            //Save Previous Entity
            BattleEntity previousEntity = turnEntity;

            //End Current Turn 
            TurnEntity?.EndTurn();

            //Select new Entity
            TurnEntity = PrepareTurnOrder();

            //Trigger Board's End Turn 
            onEndTurn?.Invoke(previousEntity);

            //Start Next Turn
            TurnEntity.StartTurn(this);

            //Assign onEndTurn to new Entity
            TurnEntity.onEndTurn =
                x =>
                {
                    //Go to Next Turn
                    NextTurn();
                };

            onStartTurn?.Invoke(turnEntity);


        }

        private bool IsBattleOver()
        {
            HashSet<SelectionTeam> aliveTeams =
                Entities.Where(x => !x.IsDead).Select(x => x.Team).ToHashSet();

            UnityEngine.Debug.Log("Teams in play: " + aliveTeams.Count);

            return aliveTeams.Count <= 1 || Turn >= 5;
        }

        protected abstract BattleEntity PrepareTurnOrder();

        // internal void PerformAction(IBattleMove move, BattleEntity user, IEnumerable<BattleEntity> targets)
        // {
        //     move?.ResolveMove(user, targets);
        // }
    }
}

