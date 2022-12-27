using System;
using System.Collections.Generic;
using Bardent.Weapons.Components;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Bardent.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        public static List<Type> dataCompTypes = new List<Type>()
        {
            typeof(MovementData),
            typeof(WeaponSpriteData)
        };

        [DidReloadScripts]
        static void OnRecompile()
        {
            Debug.Log("Recompiled");
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