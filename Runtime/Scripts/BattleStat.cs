using System;

namespace RpgEssentials.TurnBased
{
    [Serializable]
    public struct BattleStat
    {
        string name;
        float defaultValue;
        float currentValue;

        public float DefaultValue
        { get => defaultValue; set => defaultValue = value; }

        public float CurrentValue
        { get => currentValue; set => currentValue = value; }

        public string Name => name;


        public BattleStat(float defaultValue, string name)
        {
            this.name = name;
            this.defaultValue = defaultValue;
            currentValue = defaultValue;
        }

        public BattleStat(string name, BattleStat battleStat)
        {
            this.name = name;
            this.defaultValue = battleStat.defaultValue;
            this.currentValue = battleStat.currentValue;
        }

   
        public override string ToString()
        {
            return $"{name}: {currentValue}/{defaultValue}";
        }

        public static explicit operator float(BattleStat self)
        {
            return self.currentValue;
        }
    }



}





