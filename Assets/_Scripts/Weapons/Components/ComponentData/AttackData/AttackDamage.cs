using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] private float damageAmount;
    }
}