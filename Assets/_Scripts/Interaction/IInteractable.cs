using UnityEngine;

namespace Bardent.Interaction
{
    public interface IInteractable
    {
        void EnableInteraction();

        void DisableInteraction();

        Vector3 GetPosition();
    }

    public interface IInteractable<T> : IInteractable
    {
        bool TryInteract(out T context);
    }
}