using System;
using Bardent.CoreSystem;
using Bardent.Utilities;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class Charge : WeaponComponent<ChargeData, AttackCharge>
    {
        private int currentCharge;

        private TimeNotifier timeNotifier;

        private ParticleManager particleManager;

        public int CurrentCharge => currentCharge;

        public int TakeFinalChargeReading()
        {
            timeNotifier.Disable();
            return CurrentCharge;
        }
        
        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentCharge = currentAttackData.InitialChargeAmount;

            timeNotifier.Init(currentAttackData.ChargeTime, true);
        }

        private void HandleNotify()
        {
            currentCharge++;
            
            if (currentCharge >= currentAttackData.NumberOfCharges)
            {
                currentCharge = currentAttackData.NumberOfCharges;
                timeNotifier.Disable();
                particleManager.StartParticlesRelative(currentAttackData.FullyChargedIndicatorParticlePrefab,
                    currentAttackData.Offset, Quaternion.identity);
            }
            else
            {
                particleManager.StartParticlesRelative(currentAttackData.ChargeIncreaseIndicatorParticlePrefab,
                    currentAttackData.Offset, Quaternion.identity);
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

            timeNotifier = new TimeNotifier();

            timeNotifier.OnNotify += HandleNotify;
        }

        protected override void Start()
        {
            base.Start();

            particleManager = Core.GetCoreComponent<ParticleManager>();
        }

        private void Update()
        {
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