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
        private KnockBackReceiver knockBackReceiver;
        private PoiseDamageReceiver poiseDamageReceiver;

        // The modifier that we give the DamageReceiver when the block window is active.
        private BlockDamageModifier damageModifier;
        private BlockKnockBackModifier knockBackModifier;
        private BlockPoiseDamageModifier poiseDamageModifier;

        private CoreSystem.Movement movement;

        // When the animation event is invoked with the Block AnimationWindows enum as a parameter, we add the modifier
        private void HandleStartAnimationWindow(AnimationWindows window)
        {
            if (window != AnimationWindows.Block)
                return;

            damageReceiver.Modifiers.AddModifier(damageModifier);
            knockBackReceiver.Modifiers.AddModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.AddModifier(poiseDamageModifier);
            
        }

        // When the animation event is invoked with the Block AnimationWindows enum as a parameter, we remove the modifier
        private void HandleStopAnimationWindow(AnimationWindows window)
        {
            if (window != AnimationWindows.Block)
                return;

            damageReceiver.Modifiers.RemoveModifier(damageModifier);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
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
            knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
            damageReceiver = Core.GetCoreComponent<DamageReceiver>();
            poiseDamageReceiver = Core.GetCoreComponent<PoiseDamageReceiver>();

            AnimationEventHandler.OnStartAnimationWindow += HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow += HandleStopAnimationWindow;

            // Create the modifier objects.
            damageModifier = new BlockDamageModifier(IsAttackBlocked);
            knockBackModifier = new BlockKnockBackModifier(IsAttackBlocked);
            poiseDamageModifier = new BlockPoiseDamageModifier(IsAttackBlocked);

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