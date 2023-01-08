using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Bardent.Weapons
{
    public class TypeNameDropdownAttribute : PropertyAttribute
    {
        public List<Type> types = new List<Type>();
        public List<string> typeNames = new List<string>();
        
        public TypeNameDropdownAttribute(Type interfaceType)
        {
            Debug.Log("Creating new TypeNameDropdown");
            
            types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type =>
                interfaceType.IsAssignableFrom(type) && !type.IsInterface)
            .ToList();
            
            typeNames.Clear();
            
            for (var i = 0; i < types.Count; i++)
            {
                Debug.Log($"i: {i} Name: {types[i].Name}");
                typeNames.Add(types[i].Name);
            }
        }
    }
}