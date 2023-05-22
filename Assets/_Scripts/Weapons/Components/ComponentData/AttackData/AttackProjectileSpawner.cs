using System;
using Bardent.ProjectileSystem;
using Bardent.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackProjectileSpawner : AttackData
    {
        // This is an array as each attack can spawn multiple projectiles.
        [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
    }

    [Serializable]
    public struct ProjectileSpawnInfo
    {
        // Offset from the players transform
        [field: SerializeField] public Vector2 Offset { get; private set; }
        
        // Direction that the projectile spawns in, relative to the facing direction of the player
        [field: SerializeField] public Vector2 Direction { get; private set; }
        
        // The projectile prefab, notice that the type is Projectile and not GameObject
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
        
        // The damage data to be passed to the projectile when it is spawned
        [field: SerializeField] public  DamageDataPackage DamageData { get; private set; }
    }
}