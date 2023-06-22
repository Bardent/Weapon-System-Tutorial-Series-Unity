using Bardent.Utilities;

namespace Bardent.Weapons.Components
{
    public class Charge : WeaponComponent<ChargeData, AttackCharge>
    {
        private int currentCharge;

        private TimeNotifier timeNotifier;

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
                // Spawn fully charge particles
                return;
            }
            else
            {
                //spawn single charge particle
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

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            timeNotifier.OnNotify -= HandleNotify;
        }

        #endregion
    }
}