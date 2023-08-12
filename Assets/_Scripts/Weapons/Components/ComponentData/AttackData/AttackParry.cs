using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackParry : AttackData
    {
        [field: SerializeField] public float DamageAbsorption { get; private set; }
        [field: SerializeField] public Rect ParryHitBox { get; private set; }
    }
}