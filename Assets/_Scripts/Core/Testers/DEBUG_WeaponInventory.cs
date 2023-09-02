using System;
using Bardent.Weapons;
using UnityEngine;

namespace Bardent.CoreSystem.Testers
{
    public class DEBUG_WeaponInventory : MonoBehaviour
    {
        [field: SerializeField] public WeaponDataSO WeaponData { get; private set; }
        [field: SerializeField] public CombatInputs CombatInput { get; private set; }

        private WeaponInventory weaponInventory;

        private void Awake()
        {
            weaponInventory = GetComponent<WeaponInventory>();
        }

        [ContextMenu("Change Weapon")]
        private void ChangeWeaponData()
        {
            if(!Application.isPlaying)
                return;

            weaponInventory.TrySetWeapon(WeaponData, (int)CombatInput, out _);
        }
    }
}