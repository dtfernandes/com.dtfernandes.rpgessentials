using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    /// <summary>
    /// Interface that defines the conglomerate of data that pertains to a Battle 
    /// at a specific time
    /// </summary>
    /// <typeparam name="T">Entities populating the Battle</typeparam>
    public interface IBattleBoard
    {
        //Entities in play
        IEnumerable<BattleEntity> Entities { get; }
        
        //Entity in playing in the current turn
        BattleEntity TurnEntity { get; set; }

        //End current Turn and begin the next
        void NextTurn();
    }

}


