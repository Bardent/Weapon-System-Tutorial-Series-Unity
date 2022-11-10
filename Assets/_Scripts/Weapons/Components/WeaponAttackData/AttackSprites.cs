using System;
using UnityEngine;

namespace Bardent.Weapons.Components.WeaponAttackData
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}