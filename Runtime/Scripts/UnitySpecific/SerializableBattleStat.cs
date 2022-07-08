using UnityEngine;

namespace RpgEssentials.TurnBased
{
    /// <summary>
    /// Helper Struct resoncible for serializing the BattleStat struct
    /// </summary>
    [System.Serializable]
    public struct SerializableBattleStat
    {
        //Name of the BattleStat
        [SerializeField]
        private string name;

        //Default value of the BattleStat
        [SerializeField]
        private RangedInt defaultValue;
  
        /// <summary>
        /// Default value of the BatteStat
        /// </summary>
        public RangedInt DefaultValue => defaultValue;

        /// <summary>
        /// Constructor for the SerializableBattleStat struct
        /// </summary>
        /// <param name="name">Name of the stat</param>
        /// <param name="value">RangedInt component</param>
        public SerializableBattleStat(string name, RangedInt value)
        {
            this.name = name;
            this.defaultValue = value;
        }

        /// <summary>
        /// Operator that transforms a SerializedBattleStat into a BattleStat
        /// </summary>
        /// <param name="self">Given SerializedBattleStat</param>
        public static implicit operator BattleStat(SerializableBattleStat self)
        {
            BattleStat newBattleStat = 
                new BattleStat(self.defaultValue, self.name);
            return newBattleStat;
        }
    }
}