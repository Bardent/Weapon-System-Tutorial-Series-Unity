using System;
using UnityEngine;

namespace Bardent.Weapons.Components.WeaponAttackData
{
    // Base class representing the data for a single attack of a weapon component
    public abstract class AttackData
    {
        [SerializeField] private string attackName;
    }
}