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
        private static List<Type> dataCompTypes = new List<Type>();

        private WeaponDataSO weaponData;

        private void OnEnable()
        {
            weaponData = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var type in dataCompTypes)
            {
                if (GUILayout.Button(type.Name))
                {
                    var comp = Activator.CreateInstance(type) as ComponentData;

                    if (comp.GetType().IsSubclassOf(typeof(ComponentData)))
                    {
                        weaponData.AddData(comp);
                    }
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            
            var filteredTypes = types.Where(type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass);
            dataCompTypes = filteredTypes.ToList();
        }
    }
}