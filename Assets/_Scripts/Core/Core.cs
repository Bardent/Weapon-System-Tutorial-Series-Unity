using System.Collections.Generic;
using System.Linq;
using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class Core : MonoBehaviour
    {
        /*
         * The GameObject representing the root of this entity. For most of my cases the Core sits on a child GO of the root GO so awake defaults to that. But
         * you also have the option of manually assigning it
         */
        [field: SerializeField] public GameObject Root { get; private set; }
        
        private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

        private void Awake()
        {
            Root = Root ? Root : transform.parent.gameObject;
        }

        public void LogicUpdate()
        {
            foreach (CoreComponent component in CoreComponents)
            {
                component.LogicUpdate();
            }
        }

        public void AddComponent(CoreComponent component)
        {
            if (!CoreComponents.Contains(component))
            {
                CoreComponents.Add(component);
            }
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp = CoreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();

            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}