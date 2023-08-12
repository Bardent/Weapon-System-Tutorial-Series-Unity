using System;
using System.Collections.Generic;
using Bardent.CoreSystem;
using UnityEngine;
using static Bardent.Combat.Parry.CombatParryUtilities;

namespace Bardent.Weapons.Components
{
    public class Parry : WeaponComponent<ParryData, AttackParry>
    {
        public Action<List<GameObject>> OnParry;

        private CoreSystem.Movement movement;
        private ParticleManager particleManager;

        private bool isParryWindowActive;

        private Vector2 hitBoxWorldCenter;

        private void CheckParryHitBox()
        {
            var position = transform.position;

            hitBoxWorldCenter.Set(
                position.x + (currentAttackData.ParryHitBox.center.x * movement.FacingDirection),
                position.y + currentAttackData.ParryHitBox.center.y
            );

            var detected = Physics2D.OverlapBoxAll(hitBoxWorldCenter, currentAttackData.ParryHitBox.size, 0f,
                data.ParryableLayers);
            
            if(detected.Length <= 0)
                return;

            if (TryParry(detected, new Combat.Parry.ParryData(Core.Root), out _, out var parriedGameObjects))
            {
                OnParry?.Invoke(parriedGameObjects);
            }
        }

        private void HandleStartAnimationWindow(AnimationWindows window)
        {
            if(window != AnimationWindows.Parry)
                return;

            isParryWindowActive = true;
        }

        private void HandleStopAnimationWindow(AnimationWindows window)
        {
            if(window != AnimationWindows.Parry)
                return;
            
            isParryWindowActive = false;
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            particleManager = Core.GetCoreComponent<ParticleManager>();

            AnimationEventHandler.OnStartAnimationWindow += HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow += HandleStopAnimationWindow;
        }

        private void Update()
        {
            if (!isParryWindowActive)
                return;

            CheckParryHitBox();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnStartAnimationWindow -= HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow -= HandleStopAnimationWindow;
        }

        private void OnDrawGizmos()
        {
            if (data is null)
                return;

            foreach (var attackParry in data.GetAllAttackData())
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)attackParry.ParryHitBox.center, attackParry.ParryHitBox.size);
            }
        }

        #endregion
    }
}