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
        
        // We want a list of components that build up out weapon but we cannot make a list of Monobehaviours and serialize it => we need to separate weapon components and their data

    }
}