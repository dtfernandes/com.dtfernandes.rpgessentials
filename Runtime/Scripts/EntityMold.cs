using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace RpgEssentials.TurnBase
{
    [System.Serializable]
    public abstract class EntityMold
    {
        public abstract EntityMold Copy();

        public IEnumerable<BattleStat> ToList()
        {
            IEnumerable<PropertyInfo> statsInfo = GetType().GetProperties().
                    Where(x => x.PropertyType == typeof(BattleStat));

            return statsInfo.Select(x => new BattleStat(x.Name,(BattleStat)x.GetValue(this)));
        }

        public void SetAtIndex(int index, BattleStat stat)
        {
            GetType().GetProperties().
                 Where(x => x.PropertyType
                 == typeof(BattleStat)).ElementAt(index).SetValue(this, stat);
        }
        
        public void SetAtIndex(int index, float value)
        {
            
        }
    }

}



