using System;
using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private CoreComp<CoreSystem.Movement> movement;

        private Vector2 offset;

        private Collider2D[] detected;

        private void HandleAttackAction()
        {
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.x * movement.Comp.FacingDirection),
                transform.position.y + currentAttackData.HitBox.y
            );

            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
                return;

            OnDetectedCollider2D?.Invoke(detected);

            foreach (var item in detected)
            {
                Debug.Log(item.name);
            }
        }

        protected override void Start()
        {
            base.Start();

            movement = new CoreComp<CoreSystem.Movement>(Core);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            eventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null)
                return;

            foreach (var item in data.AttackData)
            {
                if (!item.Debug)
                    continue;
                
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
            }
        }
    }
}