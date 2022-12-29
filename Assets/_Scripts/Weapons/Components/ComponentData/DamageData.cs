using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        [SerializeField] private Type providerType;
        [SerializeField] private string testString;
        
    }
}