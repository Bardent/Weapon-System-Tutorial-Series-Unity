using System.Collections.Generic;
using Bardent.Combat.Damage;
using UnityEngine;

namespace Bardent.Utilities
{
    /*
     * This Utility class provides some static functions for logic we might perform in many different places. This way
     * we can keep it all consolidated here and only have to change it in one place. That is the dream anyway.
     *
     * For example: The Damage functions are called by both DamageOnHitBoxAction and DamageOnBlock weapon components.
     */
    public static class CombatDamageUtilities
    {
        public static bool TryDamage(GameObject gameObject, DamageData damageData, out IDamageable damageable)
        {
            // TryGetComponentInChildren is a custom GameObject extension method.
            if (gameObject.TryGetComponentInChildren(out damageable))
            {
                damageable.Damage(damageData);
                return true;
            }

            return false;
        }

        public static bool TryDamage(Collider2D[] colliders, DamageData damageData, out List<IDamageable> damageables)
        {
            var hasDamaged = false;
            damageables = new List<IDamageable>();
            
            foreach (var collider in colliders)
            {
                if (TryDamage(collider.gameObject, damageData, out IDamageable damageable))
                {
                    damageables.Add(damageable);
                    hasDamaged = true;
                }
            }

            return hasDamaged;
        }
    }
}