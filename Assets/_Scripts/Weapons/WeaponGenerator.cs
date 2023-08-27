using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.CoreSystem;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private CombatInputs combatInput;

        private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();

        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();

        private List<Type> componentDependencies = new List<Type>();

        private Animator anim;

        private WeaponInventory weaponInventory;

        private void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            if (data is null)
            {
                weapon.SetCanEnterAttack(false);
                return;
            }
            
            componentAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentDependencies.Clear();

            componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

            componentDependencies = data.GetAllDependencies();

            foreach (var dependency in componentDependencies)
            {
                if(componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                var weaponComponent =
                    componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }
                
                weaponComponent.Init();
                
                componentsAddedToWeapon.Add(weaponComponent);
            }

            var componentsToRemove = componentAlreadyOnWeapon.Except(componentsAddedToWeapon);
            
            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }

            anim.runtimeAnimatorController = data.AnimatorController;
            
            weapon.SetCanEnterAttack(true);
        }
        
        private void HandleWeaponDataChanged(int inputIndex, WeaponDataSO data)
        {
            if (inputIndex != (int)combatInput)
                return;
            
            GenerateWeapon(data);
        }
        
        #region Plumbing

        private void Start()
        {
            weaponInventory = weapon.Core.GetCoreComponent<WeaponInventory>();

            weaponInventory.OnWeaponDataChanged += HandleWeaponDataChanged;
            
            anim = GetComponentInChildren<Animator>();

            if (weaponInventory.TryGetWeapon((int)combatInput, out var data))
            {
                GenerateWeapon(data);
            }
        }

        private void OnDestroy()
        {
            weaponInventory.OnWeaponDataChanged -= HandleWeaponDataChanged;
        }

        #endregion
    }
}