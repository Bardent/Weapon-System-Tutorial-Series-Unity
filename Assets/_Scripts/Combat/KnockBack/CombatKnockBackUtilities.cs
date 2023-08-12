using System.Collections.Generic;
using Bardent.Combat.Parry;
using Bardent.Utilities;
using UnityEngine;

namespace Bardent.Combat.KnockBack
{
    public static class CombatKnockBackUtilities
    {
        public static bool TryKnockBack(GameObject gameObject, KnockBackData data, out IKnockBackable knockBackable)
        {
            if (gameObject.TryGetComponentInChildren(out knockBackable))
            {
                knockBackable.KnockBack(data);
                return true;
            }

            return false;
        }

        public static bool TryKnockBack(Component component, KnockBackData data, out IKnockBackable knockBackable)
        {
            return TryKnockBack(component.gameObject, data, out knockBackable);
        }

        public static bool TryKnockBack(IEnumerable<GameObject> gameObjects, KnockBackData data,
            out List<IKnockBackable> knockBackables)
        {
            var hasKnockedBack = false;
            knockBackables = new List<IKnockBackable>();

            foreach (var gameObject in gameObjects)
            {
                if (TryKnockBack(gameObject, data, out var knockBackable))
                {
                    knockBackables.Add(knockBackable);
                    hasKnockedBack = true;
                }
            }

            return hasKnockedBack;
        }
        
        public static bool TryKnockBack(IEnumerable<Component> components, KnockBackData data,
            out List<IKnockBackable> knockBackables)
        {
            var hasKnockedBack = false;
            knockBackables = new List<IKnockBackable>();

            foreach (var comp in components)
            {
                if (TryKnockBack(comp, data, out var knockBackable))
                {
                    knockBackables.Add(knockBackable);
                    hasKnockedBack = true;
                }
            }

            return hasKnockedBack;
        }
    }
}