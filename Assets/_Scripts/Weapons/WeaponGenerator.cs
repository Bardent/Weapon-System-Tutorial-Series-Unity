using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        
        private Weapon weapon;

        private List<WeaponComponent> addedComponents = new List<WeaponComponent>();
        private List<WeaponComponent> currentWeaponComponents = new List<WeaponComponent>();
        private List<Type> dependencies = new List<Type>();

        public void GenerateWeapon()
        {
            if (!WeaponHasData()) return;
            
            ResetLists();

            currentWeaponComponents = GetComponents<WeaponComponent>().ToList();
            dependencies = weapon.Data.GetAllDependencies();

            AddDependenciesToGameObject();

            RemoveUnneededWeaponComponents();

            anim.runtimeAnimatorController = weapon.Data.AnimatorController;
        }

        private void RemoveUnneededWeaponComponents()
        {
            foreach (var weaponComponent in currentWeaponComponents)
            {
#if UNITY_EDITOR
                DestroyImmediate(weaponComponent);
#else
                Destroy(weaponComponent);
#endif
            }
        }

        private void AddDependenciesToGameObject()
        {
            foreach (var dependency in dependencies)
            {
                if (addedComponents.FirstOrDefault(item => item.GetType() == dependency))
                    continue;

                var component = currentWeaponComponents.FirstOrDefault(item => item.GetType() == dependency);

                if (component == null)
                {
                    component = gameObject.AddComponent(dependency) as WeaponComponent;
                }
                else
                {
                    currentWeaponComponents.Remove(component);
                }

                addedComponents.Add(component);
            }
        }

        private void ResetLists()
        {
            addedComponents.Clear();
            currentWeaponComponents.Clear();
            dependencies.Clear();
        }

        private bool WeaponHasData()
        {
            if (weapon.Data == null)
            {
                Debug.LogError($"{transform.name} has no associated data");
                return false;
            }

            return true;
        }
        
        private void Awake()
        {
            weapon = GetComponent<Weapon>();
            
            GenerateWeapon();
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Generate Weapon")]
        private void GenerateWeaponFromEditor()
        {
            weapon = GetComponent<Weapon>();
            
            GenerateWeapon();
        }
        #endif
    }
}