using Bardent.Interaction;
using Bardent.Weapons;
using UnityEngine;

namespace Bardent.WeaponPickup
{
    public class WeaponPickup : MonoBehaviour, IInteractable<WeaponDataSO>
    {
        [SerializeField] private WeaponDataSO weaponData;

        public bool TryInteract(out WeaponDataSO context)
        {
            context = weaponData;

            return weaponData is not null;
        }

        public void EnableInteraction()
        {
            print("Enable Interaction");
        }

        public void DisableInteraction()
        {
            print("Disable Interaction");
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}