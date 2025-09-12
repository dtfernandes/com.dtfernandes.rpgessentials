using UnityEngine;
using RpgEssentials.TurnBased;
using System.Collections.Generic;


public abstract class SEntityTemplate : EntityTemplate
{
    [SerializeField]
    protected Sprite orderIcon;
    public Sprite OrderIcon => orderIcon;

}
