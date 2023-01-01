﻿using UnityEngine;

namespace Bardent.CoreSystem
{
    public class CoreComp<T> where T : CoreComponent
    {
        private Core core;
        private T comp;

        public T Comp => comp ? comp : core.GetCoreComponent(ref comp);

        public CoreComp(Core core)
        {
            if (core == null)
            {
                Debug.LogWarning($"Core is Null for component {typeof(T)}");
            }
            
            this.core = core;
        }
    }
}