using System;
using UnityEngine;

namespace Bardent.Weapons.Components.ComponentData.AttackData
{
    [Serializable]
    public class AttackSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}