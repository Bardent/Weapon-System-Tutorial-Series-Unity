using System.Collections.Generic;
using Bardent.Utilities;
using UnityEngine;

namespace Bardent.Combat.Parry
{
    public static class CombatParryUtilities
    {
        public static bool TryParry(GameObject gameObject, ParryData data, out IParryable parryable,
            out GameObject parriedGameObject)
        {
            parriedGameObject = null;

            if (gameObject.TryGetComponentInChildren(out parryable))
            {
                parriedGameObject = gameObject;
                parryable.Parry(data);
                return true;
            }

            return false;
        }

        public static bool TryParry(Component component, ParryData data, out IParryable parryable,
            out GameObject parriedGameObject)
        {
            return TryParry(component.gameObject, data, out parryable, out parriedGameObject);
        }

        public static bool TryParry<T>(T[] components, ParryData data, out List<IParryable> parryables,
            out List<GameObject> parriedGameObjects)
            where T : Component
        {
            var hasParried = false;

            parryables = new List<IParryable>();
            parriedGameObjects = new List<GameObject>();

            foreach (var component in components)
            {
                if (TryParry(component, data, out var parryable, out var parriedGameObject))
                {
                    parryables.Add(parryable);
                    parriedGameObjects.Add(parriedGameObject);
                    hasParried = true;
                }
            }

            return hasParried;
        }
    }
}