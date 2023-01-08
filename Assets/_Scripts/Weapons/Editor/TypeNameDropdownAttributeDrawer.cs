using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Bardent.Weapons
{
    [CustomPropertyDrawer(typeof(TypeNameDropdownAttribute))]
    public class TypeNameDropdownAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeData = attribute as TypeNameDropdownAttribute;

            var currentStringValue = property.stringValue;

            var selectedIndex = typeData.types.FindIndex(item => item.FullName == currentStringValue);

            if (selectedIndex == -1) 
                selectedIndex = 0;

            // var newLabel = new GUIContent("YEET");
            // EditorGUI.PropertyField(position, property, newLabel, true);

            
            selectedIndex = EditorGUI.Popup(position, selectedIndex, typeData.typeNames.ToArray());

            property.stringValue = typeData.types[selectedIndex].FullName;
        }
    }
}