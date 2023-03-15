using UnityEngine;
using RpgEssentials.TurnBased;
using System.Collections.Generic;


public abstract class SimpleEntityTemplate : EntityTemplate
{
    [SerializeField]
    protected Sprite orderIcon;
    public Sprite OrderIcon => orderIcon;

   
}
