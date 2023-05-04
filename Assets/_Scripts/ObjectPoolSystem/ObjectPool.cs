using System.Collections.Generic;
using UnityEngine;

namespace Bardent.ObjectPoolSystem
{
    public class ObjectPool
    {
        [field: SerializeField] public int StartCount { get; private set; }
        
        private Dictionary<string, object> pools = new Dictionary<string, object>();
        
        public ObjectPool<T> GetPool<T>(T prefab) where T : Component
        {
            if (!pools.ContainsKey(prefab.name))
            {
                pools[prefab.name] = new ObjectPool<T>(prefab, StartCount);
            }

            return (ObjectPool<T>)pools[prefab.name];
        }
        
        public void ReturnObject<T>(T obj) where T : Component
        {
            var objPool = GetPool(obj);
            objPool.ReturnObject(obj);
        }
    }

    public class ObjectPool<T> where T : Component
    {
        private readonly T prefab;
        private readonly Queue<T> poolStack = new Queue<T>();

        public ObjectPool(T prefab, int startCount = 0)
        {
            this.prefab = prefab;

            for (var i = 0; i < startCount; i++)
            {
                var obj = Object.Instantiate(prefab);
                obj.name = prefab.name;
                obj.gameObject.SetActive(false);
                poolStack.Enqueue(obj);
            }
        }

        public T GetObject()
        {
            if (!poolStack.TryDequeue(out var obj))
            {
                obj =  Object.Instantiate(prefab);
                obj.name = prefab.name;
                return obj;
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            poolStack.Enqueue(obj);
        }
    }
}