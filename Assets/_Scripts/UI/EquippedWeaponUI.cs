using Bardent.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Bardent.UI
{
    public class EquippedWeaponUI : MonoBehaviour
    {
        [SerializeField] private WeaponDataSO weaponData;
        [SerializeField] private Image weaponIcon;

        [ContextMenu("Set Weapon Icon")]
        private void SetWeaponIcon()
        {
            if(weaponData is null)
                return;

            weaponIcon.sprite = weaponData.Icon;
        }
    }
}