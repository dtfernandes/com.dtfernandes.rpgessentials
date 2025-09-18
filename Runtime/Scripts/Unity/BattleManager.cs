using UnityEngine;
using System;

namespace RpgEssentials.TurnBased
{
    public class BattleManager : MonoBehaviour
    {
        public static event Action OnStartBattle;

        /// <summary>
        /// Information about the current battle taking place
        /// </summary>
        public static BattleDataPacket BattleInfo { get; private set; }

        /// <summary>
        /// Board where the battle takes place
        /// </summary>
        protected BattleBoard _board;

        /// <summary>
        /// Setups a battle based on the passed data packet. Is normally called from outside the battle scene right before
        /// the player enters the battle
        /// </summary>
        /// <param name="packet"></param>
        public static void PrepareBattle(BattleDataPacket packet)
        {
            BattleInfo = packet;
            OnStartBattle = null;
        }

        public void StartBattle()
        {
            // kinda bad. In reality I should separate the func of the Simple/PressTurn boards and the abstract Board.
            // Make the abstract Board a concrete class and use the Simple/PressTurn as things to composite
            _board = new SimpleBattleBoard();

            OnStartBattle?.Invoke();

            // Add the enemies to the board
            _board.AddEntities(BattleInfo.Enemies);

            // Add the party to the board
            _board.AddEntities(BattleInfo.Party);



            _board.StartBattle();
        }

    }
}