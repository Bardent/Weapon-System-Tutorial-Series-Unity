using UnityEngine;

namespace Bardent.Weapons
{
    [RequireComponent(typeof(Weapon))]
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;

        public void GenerateWeapon()
        {
            if (weapon.Data == null)
                return;
        }
        
    }
}