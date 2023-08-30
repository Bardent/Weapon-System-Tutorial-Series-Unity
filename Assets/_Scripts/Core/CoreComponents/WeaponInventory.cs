using System;
using Bardent.Weapons;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class WeaponInventory : CoreComponent
    {
        public event Action<int, WeaponDataSO> OnWeaponDataChanged;

        [field: SerializeField] public WeaponDataSO[] weaponData { get; private set; }

        public bool TrySetWeapon(WeaponDataSO newData, int index, out WeaponDataSO oldData)
        {
            if (index >= weaponData.Length)
            {
                oldData = null;
                return false;
            }

            oldData = weaponData[index];
            weaponData[index] = newData;

            OnWeaponDataChanged?.Invoke(index, newData);

            return true;
        }

        public bool TryGetWeapon(int index, out WeaponDataSO data)
        {
            if (index >= weaponData.Length)
            {
                data = null;
                return false;
            }

            data = weaponData[index];
            return true;
        }

        public bool TryGetEmptyIndex(out int index)
        {
            for (var i = 0; i < weaponData.Length; i++)
            {
                if (weaponData[i] is not null)
                    continue;
                
                index = i;
                return true;
            }

            index = -1;
            return false;
        }
    }
}