using System;
using Bardent.CoreSystem;
using Bardent.Weapons.Modifiers.BlockModifiers;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class Block : WeaponComponent<BlockData, AttackBlock>
    {
        // Event fired off when an attack is blocked. The parameter is the GameObject of the entity that was blocked
        public event Action<GameObject> OnBlock;

        // The players DamageReceiver core component. We use the damage receiver's modifiers to modify the amount of damage successfully blocked attacks deal.
        private DamageReceiver damageReceiver;

        // The modifier that we give the DamageReceiver when the block window is active.
        private BlockDamageModifier damageModifier;

        // When the animation event is invoked with the Block AnimationWindows enum as a parameter, we add the modifier
        private void HandleStartAnimationWindow(AnimationWindows window)
        {
            if (window != AnimationWindows.Block)
                return;

            damageReceiver.Modifiers.AddModifier(damageModifier);
        }

        // When the animation event is invoked with the Block AnimationWindows enum as a parameter, we remove the modifier
        private void HandleStopAnimationWindow(AnimationWindows window)
        {
            if (window != AnimationWindows.Block)
                return;

            damageReceiver.Modifiers.RemoveModifier(damageModifier);
        }

        /*
         * The modifier is what tells us if a block was performed. It fires off an event when used. This handles that event and broadcasts
         * that information further
         */ 
        private void HandleBlock(GameObject source) => OnBlock?.Invoke(source);

        /*
         * Update the block information the modifier uses to determine if an attack gets blocked or not
         */
        protected override void HandleEnter()
        {
            base.HandleEnter();

            damageModifier.SetAttackBlock(currentAttackData);
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            AnimationEventHandler.OnStartAnimationWindow += HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow += HandleStopAnimationWindow;

            damageReceiver = Core.GetCoreComponent<DamageReceiver>();

            // Create the modifier object.
            damageModifier = new BlockDamageModifier(
                Core.GetCoreComponent<CoreSystem.Movement>(),
                Core.Root.transform
            );

            damageModifier.OnBlock += HandleBlock;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            damageModifier.OnBlock -= HandleBlock;

            AnimationEventHandler.OnStartAnimationWindow -= HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow -= HandleStopAnimationWindow;
        }

        #endregion
    }
}