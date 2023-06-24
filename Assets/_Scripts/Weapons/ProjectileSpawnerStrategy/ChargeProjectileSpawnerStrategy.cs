using System;
using Bardent.ObjectPoolSystem;
using Bardent.ProjectileSystem;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    [Serializable]
    public class ChargeProjectileSpawnerStrategy : ProjectileSpawnerStrategy
    {
        private float angleVariation;

        private int chargeAmount;

        private Vector2 currentDirection;

        public ChargeProjectileSpawnerStrategy(float angleVariation, int chargeAmount)
        {
            this.angleVariation = angleVariation;
            this.chargeAmount = chargeAmount;
        }

        public override void SpawnProjectile(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos,
            int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile)
        {
            if(chargeAmount <= 0)
                return;

            if (chargeAmount == 1)
            {
                currentDirection = projectileSpawnInfo.Direction;
            }
            else
            {
                var initialRotationQuaternion = Quaternion.Euler(0f, 0f, -((chargeAmount - 1f) / 2f * angleVariation));
                currentDirection = initialRotationQuaternion * projectileSpawnInfo.Direction;
            }

            var rotationQuaternion = Quaternion.Euler(0f, 0f, angleVariation);

            for (var i = 0; i < chargeAmount; i++)
            {
                SetSpawnPosition(spawnerPos, projectileSpawnInfo.Offset, facingDirection);
                SetSpawnDirection(currentDirection, facingDirection);
                GetProjectileAndSetPositionAndRotation(objectPools, projectileSpawnInfo.ProjectilePrefab);
                InitializeProjectile(projectileSpawnInfo, OnSpawnProjectile);

                currentDirection = rotationQuaternion * currentDirection;
            }
        }
    }
}