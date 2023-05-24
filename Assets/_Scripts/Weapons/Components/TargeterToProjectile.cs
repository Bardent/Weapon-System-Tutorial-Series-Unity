using Bardent.ProjectileSystem;
using Bardent.ProjectileSystem.DataPackages;

namespace Bardent.Weapons.Components
{
    /*
     * When a projectile is spawned, this component gets a list of all targets from the Targeter weapon component and passes that through
     * to the projectile in the targetsDataPackage.
     */
    public class TargeterToProjectile : WeaponComponent
    {
        private ProjectileSpawner projectileSpawner;
        private Targeter targeter;

        private readonly TargetsDataPackage targetsDataPackage = new TargetsDataPackage();
        
        private void HandleSpawnProjectile(Projectile projectile)
        {
            targetsDataPackage.targets = targeter.GetTargets();

            projectile.SendDataPackage(targetsDataPackage);
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            projectileSpawner = GetComponent<ProjectileSpawner>();
            targeter = GetComponent<Targeter>();

            projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            projectileSpawner.OnSpawnProjectile -= HandleSpawnProjectile;
        }

        #endregion
    }
}