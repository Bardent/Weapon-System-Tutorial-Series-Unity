using System;
using Bardent.CoreSystem;
using Bardent.Utilities;
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

        private CoreSystem.Movement movement;

        // When the animation event is invoked with the Block AnimationWindows enum as a parameter, we add the modifier
        private void HandleStartAnimationWindow(AnimationWindows window)
        {
            if (window != AnimationWindows.Block)
                return;

            damageReceiver.Modifiers.AddModifier(damageModifier);
        }

        // Checks if source falls withing any blocked regions for the current attack. Also returns the block information
        private bool IsAttackBlocked(Transform source, out BlockDirectionInformation blockDirectionInformation)
        {
            var angleOfAttacker = AngleUtilities.AngleFromFacingDirection(
                Core.Root.transform,
                source,
                movement.FacingDirection
            );

            return currentAttackData.IsBlocked(angleOfAttacker, out blockDirectionInformation);
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


        #region Plumbing

        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();

            AnimationEventHandler.OnStartAnimationWindow += HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow += HandleStopAnimationWindow;

            damageReceiver = Core.GetCoreComponent<DamageReceiver>();

            // Create the modifier object.
            damageModifier = new BlockDamageModifier(
                IsAttackBlocked
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