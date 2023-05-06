using System.Collections.Generic;
using Bardent.ObjectPoolSystem;
using Bardent.ProjectileSystem;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {
        // Working variables
        private Vector2 spawnPos;
        private Vector2 spawnDir;
        
        // Movement Core Comp needed to get FacingDirection
        private CoreSystem.Movement movement;

        // Object pool to store projectiles so we don't have to keep instantiating new ones
        private readonly ObjectPools objectPools = new ObjectPools();

        // Weapon Action Animation Event is used to trigger firing the projectiles
        private void HandleAttackAction()
        {
            foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
            {
                // Add offset to player position, accounting for FacingDirection
                spawnPos = transform.position;
                spawnPos.Set(
                    spawnPos.x + projectileSpawnInfo.Offset.x * movement.FacingDirection,
                    spawnPos.y + projectileSpawnInfo.Offset.y
                );

                // Change spawn direction based on FacingDirection
                spawnDir.Set(projectileSpawnInfo.Direction.x * movement.FacingDirection,
                    projectileSpawnInfo.Direction.y);

                // Get projectile from pool
                var currentProjectile = objectPools.GetObject(projectileSpawnInfo.ProjectilePrefab);

                // Set position, rotation, and other related info
                currentProjectile.transform.position = spawnPos;

                var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
                currentProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                currentProjectile.Reset();

                currentProjectile.SendDataPackage(projectileSpawnInfo.DamageData);

                currentProjectile.Init();
            }
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
    }
}