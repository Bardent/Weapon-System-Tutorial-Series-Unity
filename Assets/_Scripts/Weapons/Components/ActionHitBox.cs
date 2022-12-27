using System;
using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {

        private CoreComp<CoreSystem.Movement> movement;

        private void HandleAttackAction()
        {
            Debug.Log($"HandleAttackAction: Movement: {movement.Comp.FacingDirection}");   
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
    }
}