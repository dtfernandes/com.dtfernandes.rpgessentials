﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RpgEssentials.TurnBased
{
    [CreateAssetMenu(menuName = 
        RPGEssentialsPaths.generalScriptablePath +  "/BattleMove",
        fileName = "Battle Move")]
    public class ModularBattleMove : ScriptableObject, IBattleMove
    {

        [SerializeField] [HideInInspector]
        private int selectedMold;
        [SerializeField] [HideInInspector]
        private int param1;
        [SerializeField]
        private int value;


        public void ResolveMove(BattleEntity attacker, IEnumerable<BattleEntity> target)
        {
            foreach (EntityMold mold in target.Select(x => x.Mold))
            {
                mold.SetAtIndex(param1, x => {
                    x.CurrentValue -= value;
                    return x; 
                });
            }

        }
    }

}
