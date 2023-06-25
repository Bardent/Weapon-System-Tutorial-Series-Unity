using System;
using Bardent.CoreSystem;
using Bardent.Utilities;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    /*
     * The Charge Weapon Component ticks up a discrete amount of charges that can then be read by other components
     */
    public class Charge : WeaponComponent<ChargeData, AttackCharge>
    {
        private int currentCharge;

        // The timer object that reports every time the charge duration has passed.
        private TimeNotifier timeNotifier;

        private ParticleManager particleManager;

        // Disables timer that ticks up the charge before reporting the current charge
        public int TakeFinalChargeReading()
        {
            timeNotifier.Disable();
            return currentCharge;
        }
        
        // When the attack starts, reset current charges and start charge timer
        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentCharge = currentAttackData.InitialChargeAmount;

            timeNotifier.Init(currentAttackData.ChargeTime, true);
        }

        // When timer fires off notify event, increase charges. If max charges reached, disable timer.
        private void HandleNotify()
        {
            currentCharge++;
            
            //If we have reached the max charge
            if (currentCharge >= currentAttackData.NumberOfCharges)
            {
                currentCharge = currentAttackData.NumberOfCharges;
                
                // Disable timer so that it does not keep calling this function
                timeNotifier.Disable();
                
                // Spawn particles that indicate we have reached the max charge
                particleManager.StartParticlesRelative(currentAttackData.FullyChargedIndicatorParticlePrefab,
                    currentAttackData.ParticlesOffset, Quaternion.identity);
            }
            else
            {
                // Spawn particles that indicate charge level has increased
                particleManager.StartParticlesRelative(currentAttackData.ChargeIncreaseIndicatorParticlePrefab,
                    currentAttackData.ParticlesOffset, Quaternion.identity);
            }
        }

        protected override void HandleExit()
        {
            base.HandleExit();

            timeNotifier.Disable();
        }


        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            // Initialize timer
            timeNotifier = new TimeNotifier();

            // Subscribe handler to timer's notify event
            timeNotifier.OnNotify += HandleNotify;
        }

        protected override void Start()
        {
            base.Start();

            particleManager = Core.GetCoreComponent<ParticleManager>();
        }

        private void Update()
        {
            // VIP: Don't forget to tick the timer else it won't notify.
            timeNotifier.Tick();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            timeNotifier.OnNotify -= HandleNotify;
        }

        #endregion
    }
}