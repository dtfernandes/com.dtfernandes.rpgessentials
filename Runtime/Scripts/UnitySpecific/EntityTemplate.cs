using System;
using System.Collections.Generic;
using UnityEngine;

namespace RpgEssentials.TurnBased
{
    public abstract class EntityTemplate : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private GameObject battleToken;
        public GameObject BattleToken => battleToken;

        public abstract EntityMold Mold { get; }
        
        public List<SerializableBattleStat> SerializedStats { get => serializedStats; set => serializedStats = value; }

        [SerializeField] [HideInInspector]
        private List<SerializableBattleStat> serializedStats;
        [SerializeField] [HideInInspector]
        private List<bool> serializedFlattens;

        [SerializeField] [HideInInspector]
        private List<ModularBattleMove> moves;

        [SerializeField]
        int id;
        
        /// <summary>
        /// Deserialize Class
        /// </summary>
        public void OnAfterDeserialize()
        {
            #region Deserialize Stat List
            for (int i = 0; i < serializedStats.Count; i++)
            {
                Mold.SetAtIndex(i, serializedStats[i]);
            }
            #endregion

            #region Deserialize Move List
            //Clear Move List
            Mold.Moves = new List<IBattleMove> { };
            //Add elements from helper list to move list
            foreach (ModularBattleMove move in moves)
            {
                Mold.Moves.Add(move);
            }
            #endregion
        }

        /// <summary>
        /// Serialize Class
        /// </summary>
        public void OnBeforeSerialize()
        {

            if (Mold == null) return;

            #region Serialize Stat List

            #endregion

            #region Serialize Move List
            //Make sure helper list is never null
            if (moves == null) moves = new List<ModularBattleMove> { };

            //empty helper list
            moves.Clear();

            //Add moves to helper list
            foreach(IBattleMove move in Mold.Moves)
            {
                moves.Add(move as ModularBattleMove);
            }
            #endregion
        }

    }

}



