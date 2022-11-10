using System;
using Bardent.Weapons.Components.WeaponAttackData;
using UnityEngine;

namespace Bardent.Weapons.Components.Data
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; private set; }
    }
}