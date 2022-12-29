using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.Weapons.Components;
using Bardent.Weapons.Interfaces;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Bardent.Weapons
{
    [CustomPropertyDrawer(typeof(DamageData))]
    public class DamageDataPropertyDrawer : PropertyDrawer
    {
        public static List<Type> providerTypes = new List<Type>();
        public static List<string> providerTypesNames = new List<string>();

        private int selectedIndex = 0;

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            if (providerTypes != null && providerTypesNames != null)
            {
                selectedIndex = EditorGUILayout.Popup(selectedIndex, providerTypesNames.ToArray());

                var obj = property.serializedObject.targetObject;
                
                var targetObjectClassType = obj.GetType();
                
                
                var field = targetObjectClassType.GetField("ComponentData");
                
                // Debug.Log($"Property: {field.Name}");
                
                
                // if (field != null)
                // {
                //     var value = field.GetValue(obj);
                //     field.SetValue(obj, providerTypes[selectedIndex]);
                //     Debug.Log(value.ToString());
                // }
                
                // var prop = property.FindPropertyRelative("providerType");
                // Debug.Log($"Property: {prop}");
            }
            
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 200;
        }

        [DidReloadScripts]
        static void OnRecompile()
        {
            Debug.Log("OnRecompile");

            var interfaceType = typeof(ICollider2DArrayProvider);
            
            providerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    interfaceType.IsAssignableFrom(type) && !type.IsInterface)
                .ToList();

            providerTypesNames.Clear();

            Debug.Log($"Provider Types: {providerTypes.Count}");
            
            for (var i = 0; i < providerTypes.Count; i++)
            {
                Debug.Log($"i: {i} Name: {providerTypes[i].Name}");
                providerTypesNames.Add(providerTypes[i].Name);
            }
        }
    }
}