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
                projectileSpawnerStrategy.SpawnProjectile(projectileSpawnInfo, transform.position,
                    movement.FacingDirection, objectPools, OnSpawnProjectile);
            }
        }

        private void SetDefaultProjectileSpawnStrategy()
        {
            projectileSpawnerStrategy = new ProjectileSpawnerStrategy();
            // projectileSpawnerStrategy = new ChargeProjectileSpawnerStrategy();
        }

        protected override void HandleExit()
        {
            base.HandleExit();
            
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

            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnAttackAction -= HandleAttackAction;
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