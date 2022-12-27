using System;
using System.Collections.Generic;
using Bardent.Weapons.Components;
using UnityEditor;
using UnityEngine;

namespace Bardent.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        public List<Type> dataCompTypes = new List<Type>()
        {
            typeof(MovementData),
            typeof(WeaponSpriteData)
        };
        
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