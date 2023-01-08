using System;
using Bardent.Weapons.Interfaces;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        [TypeNameDropdown(typeof(ICollider2DArrayProvider))] [SerializeField]
        private string testString;

        public string ProviderType => testString;
    }
}