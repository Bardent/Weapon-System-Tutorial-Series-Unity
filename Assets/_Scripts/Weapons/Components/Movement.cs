using Bardent.Weapons.Components.ComponentData;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class Movement : WeaponComponent
    {
        private CoreSystem.Movement coreMovement;

        private CoreSystem.Movement CoreMovement =>
            coreMovement ? coreMovement : Core.GetCoreComponent(ref coreMovement);

        private MovementData data;
        
        private void HandleStartMovement()
        {
            var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
            
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.GetData<MovementData>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}