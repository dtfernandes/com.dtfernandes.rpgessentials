using System.Collections;
using System.Collections.Generic;
using System;

namespace RpgEssentials.TurnBased
{

    /// <summary>
    /// Class responsible for handling the logistics of a Battle
    /// </summary>
    /// <typeparam name="T">Entities populating the Battle</typeparam>
    public abstract class BattleBoard<T> : IBattleBoard<T> where T : IBattleEntity
    {
        public Action<T> onStartTurn { get; set; }

        public T TurnEntity { get; set; }
        public IEnumerable<T> Entities { get; set; }

        public void NextTurn()
        {
            //End Current Turn 
            TurnEntity?.EndTurn();

            //Select new Entity
            TurnEntity = PrepareTurnOrder();

            //Start Next Turn
            TurnEntity.StartTurn();

            onStartTurn?.Invoke(TurnEntity);
        }
       
        protected abstract T PrepareTurnOrder();
      
    }

}


