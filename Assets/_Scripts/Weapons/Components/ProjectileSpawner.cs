using System;
using System.Collections.Generic;
using Bardent.ObjectPoolSystem;
using Bardent.ProjectileSystem;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {
        // Event fired off for each projectile before we call the Init() function on that projectile to allow other components to also pass through some data
        public event Action<Projectile> OnSpawnProjectile;

        // Movement Core Comp needed to get FacingDirection
        private CoreSystem.Movement movement;

        // Object pool to store projectiles so we don't have to keep instantiating new ones
        private readonly ObjectPools objectPools = new ObjectPools();

        // The strategy we use to spawn a projectile
        private IProjectileSpawnerStrategy projectileSpawnerStrategy;

        public void SetProjectileSpawnerStrategy(IProjectileSpawnerStrategy newStrategy)
        {
            projectileSpawnerStrategy = newStrategy;
        }
        
        // Weapon Action Animation Event is used to trigger firing the projectiles
        private void HandleAttackAction()
        {
            foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
            {
                // Spawn projectile based on the current strategy
                projectileSpawnerStrategy.ExecuteSpawnStrategy(projectileSpawnInfo, transform.position,
                    movement.FacingDirection, objectPools, OnSpawnProjectile);
            }
        }

        private void SetDefaultProjectileSpawnStrategy()
        {
            // The default spawn strategy is the base ProjectileSpawnerStrategy class. It simply spawns one projectile based on the data per request
            projectileSpawnerStrategy = new ProjectileSpawnerStrategy();
        }

        protected override void HandleExit()
        {
            base.HandleExit();
            
            // Reset the spawner strategy every time the attack finishes in case some other component adjusted it
            SetDefaultProjectileSpawnStrategy();
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();
            
            SetDefaultProjectileSpawnStrategy();
        }

        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();

            EventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            EventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null || !Application.isPlaying)
                return;

            foreach (var item in data.AttackData)
            {
                foreach (var point in item.SpawnInfos)
                {
                    var pos = transform.position + (Vector3)point.Offset;

                    Gizmos.DrawWireSphere(pos, 0.2f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(pos, pos + (Vector3)point.Direction.normalized);
                    Gizmos.color = Color.white;
                }
            }
        }

        #endregion
    }
}