using System;
using Bardent.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bardent.Interaction.Interactables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponPickup : MonoBehaviour, IInteractable<WeaponDataSO>
    {
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

        [SerializeField] private SpriteRenderer weaponIcon;
        [SerializeField] private Bobber bobber;
        
        [SerializeField] private WeaponDataSO weaponData;
        
        public WeaponDataSO GetContext() => weaponData;
        public void SetContext(WeaponDataSO context)
        {
            weaponData = context;

            weaponIcon.sprite = weaponData.Icon;
        }

        public void Interact()
        {
            Destroy(gameObject);
        }

        public void EnableInteraction()
        {
            bobber.StartBobbing();
        }

        public void DisableInteraction()
        {
            bobber.StopBobbing();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void Awake()
        {
            Rigidbody2D ??= GetComponent<Rigidbody2D>();
            weaponIcon ??= GetComponentInChildren<SpriteRenderer>();
            
            if(weaponData is null)
                return;

            weaponIcon.sprite = weaponData.Icon;
        }
    }
}