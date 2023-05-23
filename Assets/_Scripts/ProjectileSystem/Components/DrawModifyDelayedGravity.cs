using Bardent.ProjectileSystem.DataPackages;

namespace Bardent.ProjectileSystem.Components
{
    /*
     * This component is responsible for passing information from the DrawModifierDataPackage to the DelayedGravity component.
     */
    public class DrawModifyDelayedGravity : ProjectileComponent
    {
        private DelayedGravity delayedGravity;

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not DrawModifierDataPackage drawModifierDataPackage)
                return;

            // Modify the delayed gravity distance multiplier based on draw percentage received from the weapon
            delayedGravity.distanceMultiplier = drawModifierDataPackage.DrawPercentage;
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            delayedGravity = GetComponent<DelayedGravity>();
        }

        #endregion
    }
}