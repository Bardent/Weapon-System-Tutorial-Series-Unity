using System;
using Bardent.CoreSystem;
using Bardent.Weapons.Interfaces;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>, ICollider2DArrayProvider
    {
        public event Action<Collider2D[]> OnDetectCollider2D;

        private CoreComp<CoreSystem.Movement> movement;

        private Vector2 offset;

        private Collider2D[] detected;
        
        private void HandleAttackAction()
        {
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.x * movement.Comp.FacingDirection),
                transform.position.y + currentAttackData.HitBox.y
                );

            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayer);
            
            if(detected.Length == 0)
                return;
            
            OnDetectCollider2D?.Invoke(detected);
            
            foreach (var item in detected)
            {
                Debug.Log($"Detected: {item.name}");
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
            if(data == null || currentAttackData == null || !Application.isPlaying)
                return;

            foreach (var attackActionHitBox in data.AttackData)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)attackActionHitBox.HitBox.center, attackActionHitBox.HitBox.size);
            }
        }
    }
}