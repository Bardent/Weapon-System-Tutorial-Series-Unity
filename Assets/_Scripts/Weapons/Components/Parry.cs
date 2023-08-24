using System;
using System.Collections.Generic;
using Bardent.CoreSystem;
using Bardent.ProjectileSystem.Components;
using Bardent.Utilities;
using Bardent.Weapons.Modifiers.BlockModifiers;
using UnityEngine;
using static Bardent.Combat.Parry.CombatParryUtilities;

namespace Bardent.Weapons.Components
{
    public class Parry : WeaponComponent<ParryData, AttackParry>
    {
        public event Action<GameObject> OnParry;

        private DamageReceiver damageReceiver;
        private KnockBackReceiver knockBackReceiver;
        private PoiseDamageReceiver poiseDamageReceiver;

        private BlockDamageModifier damageModifier;
        private BlockKnockBackModifier knockBackModifier;
        private BlockPoiseDamageModifier poiseDamageModifier;

        private CoreSystem.Movement movement;

        private void HandleStartAnimationWindow(AnimationWindows window)
        {
            if (window != AnimationWindows.Parry)
                return;

            damageReceiver.Modifiers.AddModifier(damageModifier);
            knockBackReceiver.Modifiers.AddModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.AddModifier(poiseDamageModifier);
        }

        private void HandleStopAnimationWindow(AnimationWindows window)
        {
            if (window != AnimationWindows.Parry)
                return;

            damageReceiver.Modifiers.RemoveModifier(damageModifier);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
        }

        protected override void HandleExit()
        {
            base.HandleExit();
            
            damageReceiver.Modifiers.RemoveModifier(damageModifier);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
        }

        private bool IsAttackParried(Transform source, out DirectionalInformation directionalInformation)
        {
            if (!TryParry(source, new Combat.Parry.ParryData(Core.Root), out _, out var parriedGameObject))
            {
                directionalInformation = null;
                return false;
            }

            weapon.Anim.SetTrigger("parry");

            OnParry?.Invoke(parriedGameObject);

            var angleOfAttacker = AngleUtilities.AngleFromFacingDirection(
                Core.Root.transform,
                source,
                movement.FacingDirection
            );

            return currentAttackData.IsBlocked(angleOfAttacker, out directionalInformation);
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            damageReceiver = Core.GetCoreComponent<DamageReceiver>();
            knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
            poiseDamageReceiver = Core.GetCoreComponent<PoiseDamageReceiver>();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();

            AnimationEventHandler.OnStartAnimationWindow += HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow += HandleStopAnimationWindow;

            // Create the modifier objects.
            damageModifier = new BlockDamageModifier(IsAttackParried);
            knockBackModifier = new BlockKnockBackModifier(IsAttackParried);
            poiseDamageModifier = new BlockPoiseDamageModifier(IsAttackParried);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            AnimationEventHandler.OnStartAnimationWindow -= HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow -= HandleStopAnimationWindow;
        }

        #endregion
    }
}