using System;
using Bardent.ObjectPoolSystem;
using Bardent.ProjectileSystem;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    public class ProjectileSpawnerStrategy : IProjectileSpawnerStrategy
    {
        // Working variables
        private Vector2 spawnPos;
        private Vector2 spawnDir;
        private Projectile currentProjectile;

        public virtual void SpawnProjectile(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos,
            int facingDirection, ObjectPools objectPools, Action<Projectile> OnSpawnProjectile)
        {
            SetSpawnPosition(spawnerPos, projectileSpawnInfo.Offset, facingDirection);
            SetSpawnDirection(projectileSpawnInfo.Direction, facingDirection);
            GetProjectileAndSetPositionAndRotation(objectPools, projectileSpawnInfo.ProjectilePrefab);
            InitializeProjectile(projectileSpawnInfo, OnSpawnProjectile);
        }

        protected virtual void GetProjectileAndSetPositionAndRotation(ObjectPools objectPools, Projectile prefab)
        {
            // Get projectile from pool
            currentProjectile = objectPools.GetObject(prefab);

            // Set position, rotation, and other related info
            currentProjectile.transform.position = spawnPos;

            var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
            currentProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        protected virtual void InitializeProjectile(ProjectileSpawnInfo projectileSpawnInfo,
            Action<Projectile> OnSpawnProjectile)
        {
            currentProjectile.Reset();

            currentProjectile.SendDataPackage(projectileSpawnInfo.DamageData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.KnockBackData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.PoiseDamageData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.SpriteDataPackage);

            OnSpawnProjectile?.Invoke(currentProjectile);

            currentProjectile.Init();
        }

        protected virtual void SetSpawnDirection(Vector2 direction, int facingDirection)
        {
            // Change spawn direction based on FacingDirection
            spawnDir.Set(direction.x * facingDirection,
                direction.y);
        }

        protected virtual void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDirection)
        {
            // Add offset to player position, accounting for FacingDirection
            spawnPos = referencePosition;
            spawnPos.Set(
                spawnPos.x + offset.x * facingDirection,
                spawnPos.y + offset.y
            );
        }
    }
}