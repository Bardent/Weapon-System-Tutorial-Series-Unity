using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private WeaponDataSO data;
        [SerializeField] private Weapon weapon;

        private List<WeaponComponent> componentsAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentDependencies = new List<Type>();

        private void Start()
        {
            GenerateWeapon(data);
        }

        public void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            InitializeListsAndDependencies(data);

            AddNewDependencies();

            RemoveOldDependencies();
            
            foreach (var component in componentsAddedToWeapon)
            {
                component.Init();
            }
        }

        private void RemoveOldDependencies()
        {
            var componentsToRemove = componentsAlreadyOnWeapon.Except(componentsAddedToWeapon);

            foreach (var component in componentsToRemove)
            {
#if UNITY_EDITOR
                DestroyImmediate(component);
#else
                Destroy(component);
#endif
            }
        }

        private void AddNewDependencies()
        {
            foreach (var dependency in componentDependencies)
            {
                if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                var weaponComponent =
                    componentsAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }

                componentsAddedToWeapon.Add(weaponComponent);
            }
        }

        private void InitializeListsAndDependencies(WeaponDataSO data)
        {
            componentsAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentDependencies.Clear();

            componentsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            componentDependencies = data.GetAllDependencies();
        }

#if UNITY_EDITOR
        [ContextMenu("Generate Weapon")]
        private void GenerateWeaponFromEditor()
        {
            GenerateWeapon(data);
        }

        [ContextMenu("Remove All Weapon Components")]
        private void RemoveAllWeaponComponents()
        {
            foreach (var component in GetComponents<WeaponComponent>())
            {
                DestroyImmediate(component);
            }
        }
#endif
    }
}