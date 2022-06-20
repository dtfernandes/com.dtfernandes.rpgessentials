using System.Collections;
using System.Collections.Generic;
using System;

namespace RpgEssentials.TurnBased
{

    /// <summary>
    /// Class responsible for handling the logistics of a Battle
    /// </summary>
    /// <typeparam name="T">Entities populating the Battle</typeparam>
    public abstract class BattleBoard<T> : IBattleBoard<T> where T : BattleEntity
    {
        public Action<T> onStartTurn { get; set; }
        public Action<T> onEndTurn { get; set; }

        public T TurnEntity { get; set; }
        public IList<T> Entities { get; set; }

        public BattleBoard()
        {
            Entities = new List<T> { };
        }
 
        /// <summary>
        /// Method responsible for add a new list of entities to 
        /// the internal list of entities.
        /// </summary>
        /// <param name="newEntities">List of entities to add.</param>
        public void AddEntities(IEnumerable<T> newEntities)
        {
            short firstId = (short)Entities.Count;
            foreach(T entity in newEntities)
            {
                entity.InBattleID = firstId;
                firstId++;
                Entities.Add(entity);
            }
        }

        public void NextTurn()
        {
            //End Current Turn 
            TurnEntity?.EndTurn();

            //Select new Entity
            TurnEntity = PrepareTurnOrder();

            //Start Next Turn
            TurnEntity.StartTurn();

            //Assign onEndTurn to new Entity
            TurnEntity.onEndTurn = 
                x => onEndTurn?.Invoke(x as T);

            onStartTurn?.Invoke(TurnEntity);
        }
       
        protected abstract T PrepareTurnOrder();
      
    }

}


