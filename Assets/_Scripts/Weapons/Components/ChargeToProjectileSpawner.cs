namespace Bardent.Weapons.Components
{
    public class ChargeToProjectileSpawner : WeaponComponent<ChargeToProjectileSpawnerData, AttackChargeToProjectileSpawner>
    {
        private ProjectileSpawner projectileSpawner;
        private Charge charge;

        private bool hasReadCharge;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            hasReadCharge = false;
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            if(newInput || hasReadCharge)
                return;

            var newStrategy =
                new ChargeProjectileSpawnerStrategy(currentAttackData.AngleVariation, charge.CurrentCharge);

            projectileSpawner.SetProjectileSpawnerStrategy(newStrategy);

            hasReadCharge = true;
        }
        
        #region Plumbing

        protected override void Start()
        {
            base.Start();

            projectileSpawner = GetComponent<ProjectileSpawner>();
            charge = GetComponent<Charge>();

            weapon.OnCurrentInputChange += HandleCurrentInputChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        }

        #endregion
    }
}