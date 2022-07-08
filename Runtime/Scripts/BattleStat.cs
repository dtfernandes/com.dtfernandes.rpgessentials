using System;

namespace RpgEssentials.TurnBased
{
    [Serializable]
    public struct BattleStat
    {
        RangedInt defaultValue;

        public int CurrentValue { get; set; }

        public int MaxValueValue { get; private set; }

        public string Name { get; }

        /// <summary>
        /// Constructor for the BattleStat class. 
        /// Used when creating definition.
        /// current and max values will be defaulted to -99
        /// </summary>
        /// <param name="defaultValue">Template value for the stat</param>
        /// <param name="name">Name of the stat</param>
        public BattleStat(RangedInt defaultValue, string name)
        {
            this.Name = name;
            this.defaultValue = defaultValue;
            
            //Value just to know when the Stat isn't being 
            //generated properly
            this.CurrentValue = -99;
            this.MaxValueValue = -99;
        }

        /// <summary>
        /// Constructor for the BattleStat class. 
        /// Not sure if this is needed but I'm to scared to remove
        /// </summary>
        /// <param name="name">Name of the stat</param>
        /// <param name="battleStat">Stat to copy</param>
        public BattleStat(string name, BattleStat battleStat)
        {
            this.Name = name;
            this.defaultValue = battleStat.defaultValue;
           
            this.CurrentValue = battleStat.CurrentValue;
            this.MaxValueValue = battleStat.MaxValueValue;

        }

        /// <summary>
        /// Randomly generate the current and maxstats from the defaultValue
        /// </summary>
        /// <returns>BattleStat with the values changed</returns>
        public BattleStat GenerateStat()
        {
            //Generate the stats
            int value = defaultValue;

            CurrentValue = value;
            MaxValueValue = value;
            return this;
        }

        public override string ToString()
        {
            return $"{Name}: {CurrentValue}/{MaxValueValue}";
        }

        public static explicit operator float(BattleStat self)
        {
            return self.CurrentValue;
        }
    }
}





