using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.Weapons.Components;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Bardent.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        public static List<Type> dataCompTypes = new List<Type>();

        private WeaponDataSO weaponData;
        
        [DidReloadScripts]
        static void OnRecompile()
        {
            dataCompTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ComponentData)) && type.IsClass && !type.ContainsGenericParameters)
                .ToList();
        }

        private void AddDataToComponentData(Type type)
        {
            var comp = Activator.CreateInstance(type);

            if (!comp.GetType().IsSubclassOf(typeof(ComponentData))) return;
            
            weaponData.AddData(comp as ComponentData);
        }

        private void OnEnable()
        {
            weaponData = target as WeaponDataSO;;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Force Update Component Names"))
            {
                foreach (var componentData in weaponData.ComponentData)
                {
                    componentData.SetComponentName();
                }
            }
            
            base.OnInspectorGUI();

            foreach (var compType in dataCompTypes)
            {
                if (GUILayout.Button(compType.Name))
                {
                    AddDataToComponentData(compType);
                }
            }
        }
    }
}