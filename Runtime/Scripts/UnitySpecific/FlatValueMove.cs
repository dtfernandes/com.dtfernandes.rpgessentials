using System.Collections.Generic;
using UnityEngine;

namespace RpgEssentials.TurnBased
{
    [CreateAssetMenu(menuName =
    RPGEssentialsPaths.generalScriptablePath + "/BattleMove/FlatMove",
    fileName = "New Flat Move")]
    public class FlatValueMove: BattleMoveTemplate
    {
        [SerializeField] [HideInInspector]
        private int param1;


        protected override void ResolveMoveAbstract(BattleEntity attacker, 
            BattleEntity target, bool check)
        {
            target.Mold.SetAtIndex(param1, x => {
                x.CurrentValue -= value;
                return x;
            });
        }

        public override IList<int> GetParams()
        {
            return new List<int> { param1 };
        }

        public override void SetParams(IList<int> parameters)
        {
            param1 = parameters[0];
        }
    }
}

