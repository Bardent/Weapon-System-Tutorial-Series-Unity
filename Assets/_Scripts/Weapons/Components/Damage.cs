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
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(currentAttackData.Amount);
                }
            }
        }

        public override void Init()
        {
            hitBox = GetComponent<ActionHitBox>();

            base.Init();
        }

        protected override void SubscribeHandlers()
        {
            base.SubscribeHandlers();
            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}