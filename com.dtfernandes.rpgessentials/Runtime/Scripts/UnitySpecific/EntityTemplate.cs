using UnityEngine;

namespace RpgEssentials.TurnBase
{
    public abstract class EntityTemplate : ScriptableObject
    {
        public abstract EntityMold Mold { get; }
    }
}



