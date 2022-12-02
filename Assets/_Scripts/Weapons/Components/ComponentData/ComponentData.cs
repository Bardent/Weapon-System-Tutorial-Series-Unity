﻿using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public abstract class ComponentData
    {
        
    }
    
    [Serializable]
    public class ComponentData<T> : ComponentData where T : AttackData
    {
        [field: SerializeField] public T[] AttackData { get; private set; }
    }
}