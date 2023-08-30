using Bardent.Interaction;
using Bardent.Interaction.Interactables;

namespace Bardent.CoreSystem
{
    public class WeaponSwap : CoreComponent
    {
        private InteractableDetector interactableDetector;
        private WeaponInventory weaponInventory;
        
        private void HandleTryInteract(IInteractable interactable)
        {
            if(interactable is not WeaponPickup weaponPickup)
                return;

            var newWeaponData = weaponPickup.GetContext();

            if (weaponInventory.TryGetEmptyIndex(out var index))
            {
                weaponInventory.TrySetWeapon(newWeaponData, index, out _);
                interactable.Interact();
            }
        }

        protected override void Awake()
        {
            base.Awake();

            interactableDetector = core.GetCoreComponent<InteractableDetector>();
            weaponInventory = core.GetCoreComponent<WeaponInventory>();
        }

        private void OnEnable()
        {
            interactableDetector.OnTryInteract += HandleTryInteract;
        }


        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryInteract;
        }
    }
}