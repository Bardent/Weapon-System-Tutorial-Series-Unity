using System;
using Bardent.Utilities;
using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    /// <summary>
    /// DelayedGravity sets the projectiles initial gravity to zero and then sets it to some value after some distance has been travelled.
    /// This gives projectiles a similar effect to the arrows in DeadCells that travel straight for some distance and then starts to drop.
    /// </summary>
    public class DelayedGravity : ProjectileComponent
    {
        [field: SerializeField] public float Distance { get; private set; } = 10f;

        private DistanceNotifier distanceNotifier = new DistanceNotifier();

        private float gravity;

        // Once projectile has travelled Distance, set gravity to Gravity value
        private void HandleNotify()
        {
            rb.gravityScale = gravity;
        }
        
        // On Init, enable the distance notifier to trigger once distance has been travelled.
        protected override void Init()
        {
            base.Init();
            
            rb.gravityScale = 0f;
            distanceNotifier.Init(transform.position, Distance);
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            gravity = rb.gravityScale;
            
            distanceNotifier.OnNotify += HandleNotify;
        }

        protected override void Update()
        {
            base.Update();
            
            distanceNotifier?.Tick(transform.position);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            distanceNotifier.OnNotify -= HandleNotify;
        }

        #endregion
    }
}