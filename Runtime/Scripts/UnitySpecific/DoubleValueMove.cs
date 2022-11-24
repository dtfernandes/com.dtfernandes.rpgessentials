using System.Collections.Generic;
using UnityEngine;

namespace RpgEssentials.TurnBased
{
    [CreateAssetMenu(menuName =
    RPGEssentialsPaths.generalScriptablePath + "/BattleMove/DoubleValue",
    fileName = "New Double Value Move")]
    public class DoubleValueMove: BattleMoveTemplate
    {
        [SerializeField] [HideInInspector]
        private int param1;
        [SerializeField] [HideInInspector]
        private int param2;

        protected override void ResolveMoveAbstract(BattleEntity attacker, 
            BattleEntity target)
        {
            target.Mold.SetAtIndex(param1, x => 
            {
                int multiplier = (int)attacker.Mold.GetStatAt(param2);
                UnityEngine.Debug.Log(target.InBattleID + " recieved: " + value * multiplier + " damage.");
                x.CurrentValue -= value * multiplier;
                return x;
            });


            
        }

        public override IList<int> GetParams()
        {
            return new List<int> { param1, param2 };
        }

        public override void SetParams(IList<int> parameters)
        {
            param1 = parameters[0];
            param2 = parameters[1];
        }
    }


}

