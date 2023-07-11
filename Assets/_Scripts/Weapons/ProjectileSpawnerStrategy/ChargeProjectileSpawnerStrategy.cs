using System;
using Bardent.ObjectPoolSystem;
using Bardent.ProjectileSystem;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    /*
     * This class represents a custom strategy used to spawn projectiles. Out ProjectileSpawner weapon component makes use of a strategy when it wants to spawn
     * projectiles. The default strategy sits in the ProjectileSpawnerStrategy class, what this class inherits from.
     *
     * This strategy modifies how we spawn projectiles by considering some other parameters, in this case, the charge amount and angle variation. Instead of spawning a single
     * projectile when the SpawnProjectile function is called, it spawns as many projectiles as we have charges. It also varies the spawn angle of each projectiles spawned
     * such that the average spawn direction is the direction passed in.
     */
    [Serializable]
    public class ChargeProjectileSpawnerStrategy : ProjectileSpawnerStrategy
    {
        // These values is set from some external source. E.G: ChargeToProjectileSpawner weapon component
        public float AngleVariation;
        public int ChargeAmount;

        // A working variable that holds the current direction we want to spawn the next projectile in the loop in
        private Vector2 currentDirection;

        public override void ExecuteSpawnStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos,
            int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile)
        {
            // If there are no charges, we don't spawn any projectiles.
            if (ChargeAmount <= 0)
                return;

            // If there is only one charge, the direction to spawn the projectile in is the same as the direction that has been passed in.
            if (ChargeAmount == 1)
            {
                currentDirection = projectileSpawnInfo.Direction;
            }
            else
            {
                /*
                 * If there are more than one charge, we need to rotate the current direction by half of the total angle variation.
                 * Total angle variation = (ChargeAmount - 1) * AngleVariation
                 * This creates the initialRotationQuaternion. By multiplying this by the passed in spawn direction, we get a new direction that
                 * has been rotated anti-clockwise by that amount.
                 */
                var initialRotationQuaternion = Quaternion.Euler(0f, 0f, -((ChargeAmount - 1f) * AngleVariation / 2f));

                // Rotate the vector to set our first spawn direction
                currentDirection = initialRotationQuaternion * projectileSpawnInfo.Direction;
            }

            // The quaternion that we will use to rotate the spawn direction by our angle variation for every projectile we spawn
            var rotationQuaternion = Quaternion.Euler(0f, 0f, AngleVariation);

            for (var i = 0; i < ChargeAmount; i++)
            {
                // Projectile spawn methods. See ProjectileSpawnerStrategy class for more details
                SpawnProjectile(projectileSpawnInfo, currentDirection, spawnerPos, facingDirection, objectPools,
                    OnSpawnProjectile);

                // Rotate the spawn direction for next projectile.
                currentDirection = rotationQuaternion * currentDirection;
            }
        }
    }
}