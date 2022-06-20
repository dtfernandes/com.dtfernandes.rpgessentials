using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    /// <summary>
    /// Interface that defines the conglomerate of data that pertains to a Battle 
    /// at a specific time
    /// </summary>
    /// <typeparam name="T">Entities populating the Battle</typeparam>
    public interface IBattleBoard<T> where T: IBattleEntity
    {
        //Entities in play
        IList<T> Entities { get; set; }
        
        //Entity in playing in the current turn
        T TurnEntity { get; set; }

        //End current Turn and begin the next
        void NextTurn();
    }

}


