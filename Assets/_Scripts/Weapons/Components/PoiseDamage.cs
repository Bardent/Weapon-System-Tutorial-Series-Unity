﻿using Bardent.Combat.PoiseDamage;
using Bardent.Interfaces;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
                {
                    poiseDamageable.DamagePoise(new Combat.PoiseDamage.PoiseDamageData(currentAttackData.Amount, Core.Root));
                }
            }
        }
        
        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();

            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}