using UnityEngine;

namespace RpgEssentials.TurnBased
{
    [System.Serializable]
    public struct SerializableBattleStat
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private float defaultValue;
        [SerializeField]
        private float currentValue;

        public SerializableBattleStat(BattleStat stat)
        {
            this.name = stat.Name;
            this.defaultValue = stat.DefaultValue;
            this.currentValue = stat.CurrentValue;
        }

        public static implicit operator BattleStat(SerializableBattleStat self)
        {
            BattleStat newBattleStat = 
                new BattleStat(self.defaultValue,self.name);
            newBattleStat.CurrentValue = self.currentValue;
            return newBattleStat;
        }
    }
}