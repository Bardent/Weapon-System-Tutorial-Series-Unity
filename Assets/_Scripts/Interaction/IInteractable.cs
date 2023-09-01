using UnityEngine;

namespace Bardent.Interaction
{
    public interface IInteractable
    {
        void EnableInteraction();

        void DisableInteraction();

        Vector3 GetPosition();

        void Interact();
    }

    public interface IInteractable<T> : IInteractable
    {
        T GetContext();

        void SetContext(T context);
    }
}