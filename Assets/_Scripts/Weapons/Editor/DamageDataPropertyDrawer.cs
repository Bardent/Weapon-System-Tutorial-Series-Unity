// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Bardent.Weapons.Components;
// using Bardent.Weapons.Interfaces;
// using NUnit.Framework;
// using UnityEditor;
// using UnityEditor.Callbacks;
// using UnityEngine;
//
// namespace Bardent.Weapons
// {
//     [CustomPropertyDrawer(typeof(DamageData))]
//     public class DamageDataPropertyDrawer : PropertyDrawer
//     {
//         public static List<Type> providerTypes = new List<Type>();
//         public static List<string> providerTypesNames = new List<string>();
//
//         private int selectedIndex = 0;
//
//         public override void OnGUI(Rect position, SerializedProperty property,
//             GUIContent label)
//         {
//             if (providerTypes != null && providerTypesNames != null)
//             {
//                 selectedIndex = EditorGUILayout.Popup(selectedIndex, providerTypesNames.ToArray());
//
//                 var obj = property.serializedObject.targetObject as WeaponDataSO;
//                 
//                 if(obj == null)
//                     return;
//
//                 var data = obj.GetData<DamageData>();
//
//                 if (data == null)
//                     return;
//
//                 data.testString = providerTypes[selectedIndex].FullName;
//
//             }
//             
//             EditorGUI.PropertyField(position, property, label, true);
//         }
//
//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             return base.GetPropertyHeight(property, label) + 200;
//         }
//
//         [DidReloadScripts]
//         static void OnRecompile()
//         {
//             var interfaceType = typeof(ICollider2DArrayProvider);
//             
//             providerTypes = AppDomain.CurrentDomain.GetAssemblies()
//                 .SelectMany(assembly => assembly.GetTypes())
//                 .Where(type =>
//                     interfaceType.IsAssignableFrom(type) && !type.IsInterface)
//                 .ToList();
//
//             providerTypesNames.Clear();
//             
//             for (var i = 0; i < providerTypes.Count; i++)
//             {
//                 Debug.Log($"i: {i} Name: {providerTypes[i].Name}");
//                 providerTypesNames.Add(providerTypes[i].Name);
//             }
//         }
//     }
// }