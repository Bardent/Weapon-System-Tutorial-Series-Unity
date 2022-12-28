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

        private bool showForceUpdateButtons;
        private bool showAddComponentButtons;

        [DidReloadScripts]
        static void OnRecompile()
        {
            dataCompTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    type.IsSubclassOf(typeof(ComponentData)) && type.IsClass && !type.ContainsGenericParameters)
                .ToList();
        }

        private void AddDataToComponentData(Type type)
        {
            var comp = Activator.CreateInstance(type) as ComponentData;

            if (!comp.GetType().IsSubclassOf(typeof(ComponentData))) return;

            weaponData.AddData(comp);
            
            comp.InitializeAttackData(weaponData.NumberOfAttacks);
        }

        private void OnEnable()
        {
            weaponData = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Set Number Of Attacks"))
            {
                foreach (var componentData in weaponData.ComponentData)
                {
                    componentData.InitializeAttackData(weaponData.NumberOfAttacks);
                }
            }

            base.OnInspectorGUI();

            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");

            if (showAddComponentButtons)
            {
                foreach (var compType in dataCompTypes)
                {
                    if (GUILayout.Button(compType.Name))
                    {
                        AddDataToComponentData(compType);
                    }
                }
            }

            showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update Buttons");

            if (showForceUpdateButtons)
            {
                if (GUILayout.Button("Force Update Component Names"))
                {
                    foreach (var componentData in weaponData.ComponentData)
                    {
                        componentData.SetComponentName();
                    }
                }

                if (GUILayout.Button("Force Update Attack Names"))
                {
                    foreach (var componentData in weaponData.ComponentData)
                    {
                        componentData.SetAttackDataNames();
                    }
                }
            }
        }
    }
}