using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

namespace RpgEssentials.TurnBased
{

    public abstract class BattleMoveTemplate : ScriptableObject, IBattleMove
    {

        [SerializeField]
        private Sprite moveIcon;
        public Sprite MoveIcon => moveIcon;

        [SerializeField] [HideInInspector]
        private int selectedMold;
        [SerializeField]
        protected int value;
        [SerializeField]
        private string identifier;
        public string Identifier => identifier;

        [Header("Conditions")]
        [SerializeField]
        private List<ConditionPacket> conditions;

        [Header("Costs")]
        [Header("Config")]
        [SerializeField]
        private SelectionMode mode;
        [SerializeField]
        private SelectionTeam team;

        public SelectionTeam Team => team;
        public SelectionMode Mode => mode;

       

        public abstract IList<int> GetParams();
        public abstract void SetParams(IList<int> parameters);

        protected abstract void ResolveMoveAbstract
            (BattleEntity attacker, BattleEntity target);

        public virtual void ResolveMove(BattleEntity attacker, IEnumerable<BattleEntity> target)
        {
            foreach (BattleEntity be in target)
            {
                ResolveMoveAbstract(attacker, be);
                be.QuerryVitality();
            }
        }

        public bool PassesCondition(IBattleBoard battleBoard, BattleEntity x, BattleMoveTemplate template)
        {
            bool passes = true;

            foreach(ConditionPacket packet in conditions)
            {
                if(!packet.GetCondition().Condition(battleBoard , x, template))
                {
                    passes = false;
                    break;
                }
            }

            return passes;
        }
    }


    public interface ICondition
    {
        bool Condition(IBattleBoard board,
                BattleEntity target,
                    IBattleMove move);
    }

    [System.Serializable]
    public class StatCondition : ICondition
    {
        [SerializeField]
        int attackerParam;
        [SerializeField]
        int targetParam;
        [SerializeField]
        int selectedMold;
        [SerializeField]
        int selectedOperator;


        public bool Condition(
            IBattleBoard board, BattleEntity target, IBattleMove move)
        {
            int attackerValue =
                (int)board.TurnEntity.Mold.GetStatAt(attackerParam);

            int targeValue =
                (int)target.Mold.GetStatAt(targetParam);

            return attackerValue > targeValue;
        }
    }

    [System.Serializable]
    public class Cost : ICondition
    {
        [SerializeField]
        int cost;

        [SerializeField]
        int targetParam;

        public Cost(int cost, int targetParam)
        {
            this.cost = cost;
            this.targetParam = targetParam;
        }

        public bool Condition(IBattleBoard board, BattleEntity target, IBattleMove move)
        {
            return board.TurnEntity.Mold.GetStatAt(targetParam).CurrentValue >= cost;
        }
    }

    [System.Serializable]
    public class Cooldown : ICondition
    {
        public bool Condition(IBattleBoard board, BattleEntity target, IBattleMove move)
        {
            throw new System.NotImplementedException();
        }
    }

    [System.Serializable]
    public class BoardCondition : ICondition
    {
        public bool Condition(IBattleBoard board, BattleEntity target, IBattleMove move)
        {
            throw new System.NotImplementedException();
        }
    }



    [System.Serializable]
    public class ConditionPacket
    {
        [SerializeField] public List<FieldInfo> intList;
        [SerializeField] private int sizeMulti;
        //List of condition types based on reflection
        [SerializeField] private List<int> values;
        
        [SerializeField] private int conditionIndex;


        public ICondition GetCondition()
        { 
            //Create Name List of Possible Conditions                    
            var type = typeof(ICondition);

            Type[] conditions = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => type.IsAssignableFrom(p)
                   && !p.IsInterface && !p.IsAbstract).ToArray();

            Type selectedType = conditions[conditionIndex];

            ICondition cond = null;

            if (selectedType == typeof(Cost))
            {
                cond = new Cost(values[0], values[1]);
            }

            return cond;
        }
    }
}

