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

        [SerializeField] [HideInInspector]
        private List<SerializableBattleStat> serializedStats;
        [SerializeField] [HideInInspector]
        private List<ModularBattleMove> moves;

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
            #region Serialize Stat List
            //Make sure the helper list isnt null
            if (serializedStats == null)
                serializedStats = new List<SerializableBattleStat> { };

            //empty helper list
            serializedStats.Clear();

            //Add mold stats to helper list
            foreach (BattleStat bs in Mold.ToList())
            {
                serializedStats.Add(new SerializableBattleStat(bs));
            }

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



