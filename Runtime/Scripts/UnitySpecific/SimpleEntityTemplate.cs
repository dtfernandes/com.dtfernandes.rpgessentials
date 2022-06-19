using UnityEngine;
using RpgEssentials.TurnBased;
using System.Collections.Generic;


public abstract class SimpleEntityTemplate : EntityTemplate
{
    [SerializeField]
    private Sprite orderIcon;
    public Sprite OrderIcon => orderIcon;

   
}
