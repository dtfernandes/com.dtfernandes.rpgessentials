using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RpgEssentials.TurnBased
{

    /// <summary>
    /// Class responsible for handling the logistics of a Battle
    /// </summary>
    /// <typeparam name="T">Entities populating the Battle</typeparam>
    public abstract class BattleBoard<T> : IBattleBoard where T : BattleEntity
    {
        public Action<T> onStartTurn { get; set; }
        public Action<T> onEndTurn { get; set; }

        protected T turnEntity { get; set; }
        protected IList<T> entities { get; set; }

        public BattleEntity TurnEntity 
        { get => turnEntity; set => turnEntity = value as T; }

        public IEnumerable<BattleEntity> Entities{ get => entities; }

        public BattleBoard()
        {
            entities = new List<T> { };
        }
 
        /// <summary>
        /// Method responsible for add a new list of entities to 
        /// the internal list of entities.
        /// </summary>
        /// <param name="newEntities">List of entities to add.</param>
        public void AddEntities(IEnumerable<T> newEntities)
        {
            short firstId = (short)entities.Count;
            foreach(T entity in newEntities)
            {
                entity.InBattleID = firstId;
                firstId++;
                entities.Add(entity);
            }
        }

        public T GetEntityFromId(int id)
        {
            return entities.First(x => x.InBattleID == id);
        }

        public void BeginBattle()
        {
            //SetupBoard
            NextTurn();
        }

        public void NextTurn()
        {
            //Save Previous Entity
            T previousEntity = turnEntity;
            
            //End Current Turn 
            TurnEntity?.EndTurn();

            //Select new Entity
            TurnEntity = PrepareTurnOrder();

            //Trigger Board's End Turn 
            onEndTurn?.Invoke(previousEntity);

            //Start Next Turn
            TurnEntity.StartTurn();

            //Assign onEndTurn to new Entity
            TurnEntity.onEndTurn =
                x =>
                {
                    //Go to Next Turn
                    NextTurn();
                };

            onStartTurn?.Invoke(turnEntity);
        }
       
        protected abstract T PrepareTurnOrder();

    }
}

