using System;
using Bardent.Weapons.Interfaces;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ICollider2DArrayProvider provider;
        
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            print($"Handling Detected Colliders: {colliders}");
        }

        protected override void Awake()
        {
            base.Awake();

            print($"Attempting to create Type from string: {data.ProviderType}");
            
            var t = Type.GetType(data.ProviderType);
            
            print($"Got type: {t}");    

            provider = GetComponent(t) as ICollider2DArrayProvider;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            provider.OnDetectCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            provider.OnDetectCollider2D -= HandleDetectCollider2D;
        }
    }
}