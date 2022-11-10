using UnityEngine;

namespace Bardent._Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapons/Base Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject
    {
        public int NumberOfAttacks => numberOfAttacks;
        public float AttackCounterResetTimer => attackCounterResetCooldown;
        
        [SerializeField] private int numberOfAttacks;
        [SerializeField] private float attackCounterResetCooldown;

    }
}