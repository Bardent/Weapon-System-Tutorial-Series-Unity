using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
        
        [field: SerializeField] public int NumberOfAttacks { get; private set; }

        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public void AddData(ComponentData data)
        {
            if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) 
                return;
            
            ComponentData.Add(data);
        }

        public List<Type> GetAllDependencies()
        {
            var dependencies = new List<Type>();

            foreach (var componentData in ComponentData)
            {
                dependencies.Add(componentData.Dependency);
            }

            return dependencies;
        }
    }
}