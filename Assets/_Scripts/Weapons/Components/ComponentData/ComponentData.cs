using System;
using UnityEngine;

namespace Bardent.Weapons.Components.Data
{
    // Represents all the data for a weapon component
    // Consists of general settings for the component and then a list of settings for each attack (example different attacks do different amount of damage)
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField] private string componentName;
    }
}