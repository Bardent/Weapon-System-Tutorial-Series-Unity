using Bardent.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bardent.UI
{
    public class WeaponInfoUI : MonoBehaviour
    {
        [Header("Dependencies")] [SerializeField]
        private Image weaponIcon;

        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private TMP_Text weaponDescription;

        private WeaponDataSO weaponData;

        public void PopulateUI(WeaponDataSO data)
        {
            if(data is null)
                return;

            weaponData = data;

            weaponIcon.sprite = weaponData.Icon;
            weaponName.SetText(weaponData.Name);
            weaponDescription.SetText(weaponData.Description);
        }
    }
}