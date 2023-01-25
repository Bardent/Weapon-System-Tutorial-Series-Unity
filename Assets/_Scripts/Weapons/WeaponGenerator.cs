using System;
using System.Collections.Generic;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;

        private List<WeaponComponent> componentsAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentDependencies = new List<Type>();

        public void GenerateWeapon(WeaponDataSO data)
        {
            
        }
    }
}