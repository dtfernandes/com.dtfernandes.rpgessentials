using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace RpgEssentials.TurnBase
{
    //[Serializable]
    //public abstract class UnityEntityMold : IEntityMold
    //{
    //    public abstract IEnumerable<BattleStat> Stats { get; }
    //}

    //[Serializable]
    //public class ExampleEntityMold : UnityEntityMold
    //{
    //    public override IEnumerable<BattleStat> Stats => battleStats.ToList();


    //    [SerializeField]
    //    ExampleConcreteStat battleStats;
    //}

    //[Serializable]
    //public struct ExampleConcreteStat
    //{
    //    [Serializable]
    //    private struct SerializedBattleStat
    //    {
    //        [UnityEngine.SerializeField]
    //        float defaultValue;

    //        public SerializedBattleStat(float defaultValue)
    //        {
    //            this.defaultValue = defaultValue;
    //        }

    //        public float DefaultValue => defaultValue;
       
    //        public static implicit operator SerializedBattleStat(BattleStat other)
    //        {
    //            return new SerializedBattleStat(other.DefaultValue);
    //        }
    //    }

    //    [SerializeField] private int hp;
    //    [SerializeField] private int mp;
    //    [SerializeField] private int bp;
    //    [SerializeField] private int atk;
    //    [SerializeField] private int def;
    //    [SerializeField] private int speed;
    //    [SerializeField] private int maxTurns;

    //    public int Speed => speed;
    //    public int MaxTurns => maxTurns;

    //    public IEnumerable<BattleStat> ToList()
    //    {
    //        IEnumerable<int> stats =
    //            new List<int> { hp, mp, bp, atk, def, speed, maxTurns };

    //        return stats.Select(x => new BattleStat(x, "no name"));
    //    }
   
    //    public static ExampleConcreteStat Decrypt(IEnumerable<BattleStat> stats)
    //    {
    //        return new ExampleConcreteStat(stats);
    //    }
        
    //    private ExampleConcreteStat(IEnumerable<BattleStat> stats)
    //    {
    //        List<BattleStat> stat = stats.ToList();
    //        hp = (int)stat[0];
    //        mp = (int)stat[1];
    //        bp = (int)stat[2];
    //        atk = (int)stat[3];
    //        def = (int)stat[4];
    //        speed = (int)stat[5];
    //        maxTurns = (int)stat[6];
    //    }

    //}

}



