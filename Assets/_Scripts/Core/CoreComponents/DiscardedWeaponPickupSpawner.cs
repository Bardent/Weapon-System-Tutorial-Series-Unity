using System;
using Bardent.Interaction.Interactables;
using Bardent.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bardent.CoreSystem
{
    public class DiscardedWeaponPickupSpawner : CoreComponent
    {
        [SerializeField] private Vector2 spawnDirection;
        [SerializeField] private float spawnVelocity;
        [SerializeField] private WeaponPickup weaponPickupPrefab;
        [SerializeField] private Vector2 spawnOffset;

        private WeaponSwap weaponSwap;
        private Movement movement;

        private void HandleWeaponDiscarded(WeaponDataSO discardedWeaponData)
        {
            var spawnPoint = movement.FindRelativePoint(spawnOffset);
            var weaponPickup = Instantiate(weaponPickupPrefab, spawnPoint, Quaternion.identity);

            weaponPickup.SetContext(discardedWeaponData);

            var adjustedSpawnDirection = new Vector2(
                spawnDirection.x * movement.FacingDirection,
                spawnDirection.y
            );
            
            weaponPickup.Rigidbody2D.velocity = adjustedSpawnDirection.normalized * spawnVelocity;
        }

        protected override void Awake()
        {
            base.Awake();

            weaponSwap = core.GetCoreComponent<WeaponSwap>();
            movement = core.GetCoreComponent<Movement>();
        }

        private void OnEnable()
        {
            weaponSwap.OnWeaponDiscarded += HandleWeaponDiscarded;
        }

        private void OnDisable()
        {
            weaponSwap.OnWeaponDiscarded -= HandleWeaponDiscarded;
        }
    }
}