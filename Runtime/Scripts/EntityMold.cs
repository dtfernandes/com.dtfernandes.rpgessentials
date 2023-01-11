using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace RpgEssentials.TurnBased
{
    [System.Serializable]
    public abstract class EntityMold
    {
        private List<IBattleMove> moves;
        public List<IBattleMove> Moves { get => moves; set => moves = value; }

        public abstract bool IsPresistent { get; protected set; }

        public virtual string EntityName { get; protected set; }

        //Constructor for the EntityMold class
        public EntityMold()
        {
            moves = new List<IBattleMove> { };
        }

        public abstract EntityMold Copy();

        public IEnumerable<BattleStat> ToList()
        {
            IEnumerable<PropertyInfo> statsInfo = GetType().GetProperties().
                    Where(x => x.PropertyType == typeof(BattleStat));

            return statsInfo.Select(x => new BattleStat(x.Name,(BattleStat)x.GetValue(this)));
        }

        public void ReplaceAll(IEnumerable<BattleStat> stats)
        {
            int i = 0;
            foreach(BattleStat bs in stats)
            {
                SetAtIndex(i, bs);
                i++;
            }
        }

        public void SetAtIndex(int index, System.Func<BattleStat, BattleStat> func)
        {
            SetAtIndex(index, 
                func?.Invoke(GetStatAt(index)) ?? new BattleStat());
        }

        public void SetAtIndex(int index, BattleStat stat)
        {
            GetType().GetProperties().
                 Where(x => x.PropertyType
                 == typeof(BattleStat)).ElementAt(index).SetValue(this, stat);
        }  

        public BattleStat GetStatAt(int index)
        {
            return ToList().ElementAt(index);
        }
    }
}



