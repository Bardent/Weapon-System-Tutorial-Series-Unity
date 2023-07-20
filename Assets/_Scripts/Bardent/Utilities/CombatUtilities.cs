using Bardent.Combat.Damage;
using UnityEngine;

namespace Bardent.Utilities
{
    public static class CombatUtilities
    {
        public static void Damage(GameObject gameObject, DamageData damageData)
        {
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