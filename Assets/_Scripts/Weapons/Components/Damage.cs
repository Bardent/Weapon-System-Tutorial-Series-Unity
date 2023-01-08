using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBox hitBox;
        
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                print($"Detected Item: {item.name}");
            }
        }

        protected override void Awake()
        {
            base.Awake();
            hitBox = GetComponent<ActionHitBox>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}