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

        [DidReloadScripts]
        static void OnRecompile()
        {
            dataCompTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ComponentData)) && type.IsClass && !type.ContainsGenericParameters)
                .ToList();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var compType in dataCompTypes)
            {
                if (GUILayout.Button(compType.Name))
                {
                    Debug.Log($"Presses: {compType.Name}");
                }
            }
        }
    }
}