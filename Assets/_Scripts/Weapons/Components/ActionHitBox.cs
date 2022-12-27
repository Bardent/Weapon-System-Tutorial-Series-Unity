using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private void HandleAttackAction()
        {
            Debug.Log("HandleAttackAction");   
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