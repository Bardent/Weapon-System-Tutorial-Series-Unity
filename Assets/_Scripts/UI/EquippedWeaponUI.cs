using System;
using Bardent.CoreSystem;
using Bardent.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Bardent.UI
{
    public class EquippedWeaponUI : MonoBehaviour
    {
        [SerializeField] private WeaponDataSO weaponData;
        [SerializeField] private Image weaponIcon;

        [SerializeField] private CombatInputs input;
        [SerializeField] private WeaponInventory weaponInventory;

        [ContextMenu("Set Weapon Icon")]
        private void SetWeaponIcon()
        {
            weaponIcon.sprite = weaponData ? weaponData.Icon : null;
        }

        private void HandleWeaponDataChanged(int inputIndex, WeaponDataSO data)
        {
            if (inputIndex != (int)input)
                return;

            weaponData = data;
            SetWeaponIcon();
        }

        private void Start()
        {
            weaponInventory.TryGetWeapon((int)input, out weaponData);
            SetWeaponIcon();
        }

        private void OnEnable()
        {
            weaponInventory.OnWeaponDataChanged += HandleWeaponDataChanged;
        }

        private void OnDisable()
        {
            weaponInventory.OnWeaponDataChanged -= HandleWeaponDataChanged;
        }
    }
}