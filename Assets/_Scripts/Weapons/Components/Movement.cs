using UnityEngine;
using Bardent.Core.CoreComponents;

namespace Bardent.Weapons.Components
{
    public class Movement : WeaponComponent
    {
        private Core.CoreComponents.Movement movement;
        private Core.CoreComponents.Movement CoreMovement => movement ? movement : Core.GetCoreComponent(ref movement);
        
        private void HandleStartMovement()
        {
            
        }

        private void HandleStopMovement()
        {
            
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnStartMovement += HandleStartMovement;
            EventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            EventHandler.OnStartMovement -= HandleStartMovement;
            EventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}