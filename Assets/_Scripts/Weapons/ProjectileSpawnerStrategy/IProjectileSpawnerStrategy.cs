using System;
using Bardent.ObjectPoolSystem;
using Bardent.ProjectileSystem;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    public interface IProjectileSpawnerStrategy
    {
        void SpawnProjectile(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos, int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile);
        
        
    }
}