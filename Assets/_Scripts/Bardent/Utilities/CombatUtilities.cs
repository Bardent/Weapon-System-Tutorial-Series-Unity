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
    public static class CombatUtilities
    {
        public static void Damage(GameObject gameObject, DamageData damageData)
        {
            // TryGetComponentInChildren is a custom GameObject extension method.
            if (gameObject.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(damageData);
            }
        }

        public static void Damage(Collider2D[] colliders, DamageData damageData)
        {
            foreach (var collider in colliders)
            {
                Damage(collider.gameObject, damageData);
            }
        }
    }
}