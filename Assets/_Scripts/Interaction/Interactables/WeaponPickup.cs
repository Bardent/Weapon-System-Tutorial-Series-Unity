using System;
using Bardent.Weapons;
using UnityEngine;

namespace Bardent.Interaction.Interactables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponPickup : MonoBehaviour, IInteractable<WeaponDataSO>
    {
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

        [SerializeField] private WeaponDataSO weaponData;
        
        public WeaponDataSO GetContext() => weaponData;
        public void SetContext(WeaponDataSO context)
        {
            weaponData = context;
        }

        public void Interact()
        {
            Destroy(gameObject);
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

        private void Awake()
        {
            Rigidbody2D ??= GetComponent<Rigidbody2D>();
        }
    }
}