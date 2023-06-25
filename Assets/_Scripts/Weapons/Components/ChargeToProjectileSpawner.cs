﻿using UnityEngine;

namespace Bardent.Weapons.Components
{
    /*
     * This weapon component is responsible for increasing the number of projectiles that are spawned based on the attack charge.
     * It interacts with the Charge and ProjectileSpawner weapon components. When we release input it reads the charge level from the Charge
     * Component and then changes the ProjectileSpawnerStrategy on the ProjectileSpawner component to a custom strategy that increases the number
     * of projectiles spawned.
     *
     * TIP: The Strategy Pattern - https://refactoring.guru/design-patterns/strategy
     */
    public class
        ChargeToProjectileSpawner : WeaponComponent<ChargeToProjectileSpawnerData, AttackChargeToProjectileSpawner>
    {
        private ProjectileSpawner projectileSpawner;
        private Charge charge;

        private bool hasReadCharge;

        /*
         * The custom strategy we use to spawn projectiles based on the number of charges we have. To broaden your mind a little, we could encapsulate the strategy
         * in a scriptable object and allow us to spawn out the custom strategy that this component used. It's not needed here, but there are applications. E.G: I did that
         * in my game jam project (https://bardent.itch.io/the-road-to-sanctuary) to assign reload strategies to weapons
         */
        private ChargeProjectileSpawnerStrategy chargeProjectileSpawnerStrategy = new ChargeProjectileSpawnerStrategy();

        protected override void HandleEnter()
        {
            base.HandleEnter();

            hasReadCharge = false;
        }

        // Handles input change. Performs action when input is false
        private void HandleCurrentInputChange(bool newInput)
        {
            if (newInput || hasReadCharge)
                return;

            // Set the current information in the strategy
            chargeProjectileSpawnerStrategy.AngleVariation = currentAttackData.AngleVariation;
            chargeProjectileSpawnerStrategy.ChargeAmount = charge.TakeFinalChargeReading();

            // Set the strategy
            projectileSpawner.SetProjectileSpawnerStrategy(chargeProjectileSpawnerStrategy);

            // Turns off handle function till the next attack
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