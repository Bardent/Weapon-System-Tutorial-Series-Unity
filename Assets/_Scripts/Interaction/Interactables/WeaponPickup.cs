using Bardent.Weapons;
using UnityEngine;

namespace Bardent.Interaction.Interactables
{
    public class WeaponPickup : MonoBehaviour, IInteractable<WeaponDataSO>
    {
        [SerializeField] private WeaponDataSO weaponData;

        public WeaponDataSO GetContext() => weaponData;

        public void Interact()
        {
            gameObject.SetActive(false);
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