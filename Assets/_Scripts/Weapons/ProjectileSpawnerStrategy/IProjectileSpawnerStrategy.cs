using System;
using Bardent.ObjectPoolSystem;
using Bardent.ProjectileSystem;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    /*
     * The ProjectileSpawnerStrategy interface. We have a single function that takes in the ProjectileSpawnInfo, the position of the spawner, the facingDirection of the spawner,
     * the object pool to get the projectile from, and an action to invoke when a projectile is spawned.
     */
    public interface IProjectileSpawnerStrategy
    {
        void ExecuteSpawnStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos, int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile);
        
        
    }
}