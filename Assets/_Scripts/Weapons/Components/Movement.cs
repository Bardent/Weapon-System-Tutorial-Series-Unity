using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class Movement : WeaponComponent
    {
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