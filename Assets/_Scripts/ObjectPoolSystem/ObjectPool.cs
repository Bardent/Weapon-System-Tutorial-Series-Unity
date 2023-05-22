using System.Collections.Generic;
using Bardent.Interfaces;
using UnityEngine;

namespace Bardent.ObjectPoolSystem
{
    /*
     * Abstract definition of the generic object pool class. It allows us to call the Release function without having to know
     * the specific type the object pool is responsible for storing. In the ObjectPools class we also use it in the dictionary of
     * object pools.
     */
    public abstract class ObjectPool
    {
        public abstract void Release();

        public abstract void ReturnObject(Component comp);
    }

    /*
     * Generic definition of ObjectPool. It is generic so that we can define the specific object we are interested in interacting with.
     * For example, we will have many different projectile prefabs with different components attached to it, but the only component we are
     * interested in working with when we spawn a projectile, is the Projectile component attached to it. So instead of returning the GameObject
     * and having to call GetComponent every time we fire a projectile, we store it as Projectile.
     */
    public class ObjectPool<T> : ObjectPool where T : Component
    {
        /*
         * When the pool is created we pass in the prefab that should be used to create new components. Remember that prefabs
         * can be stored as any of the components attached to it and does not have to be stored in a GameObject variable
         */
        private readonly T prefab;

        // The inactive object we can return when one is requested
        private readonly Queue<T> pool = new Queue<T>();

        // All the objects that are part of this pool. Inactive and active, if they have a component that implements the IObjectPoolItem interface
        private readonly List<IObjectPoolItem> allItems = new List<IObjectPoolItem>();

        // Constructor. Defines the prefab and initializes some components.
        public ObjectPool(T prefab, int startCount = 1)
        {
            this.prefab = prefab;

            for (var i = 0; i < startCount; i++)
            {
                var obj = InstantiateNewObject();

                pool.Enqueue(obj);
            }
        }

        // Instantiates a new component when needed
        private T InstantiateNewObject()
        {
            var obj = Object.Instantiate(prefab);
            obj.name = prefab.name;

            if (!obj.TryGetComponent<IObjectPoolItem>(out var objectPoolItem))
            {
                Debug.LogWarning($"{obj.name} does not have a component that implements IObjectPoolItem");
                return obj;
            }

            // If object has the IObjectPool interface, set this ObjectPool as it's pool and store in list
            objectPoolItem.SetObjectPool(this, obj);
            allItems.Add(objectPoolItem);

            return obj;
        }

        public T GetObject()
        {
            // Try to get item from the queue. TryDequeue returns true if object available and false if not
            if (!pool.TryDequeue(out var obj))
            {
                // If not available, instantiate a new one and return
                obj = InstantiateNewObject();
                return obj;
            }

            // If available, return
            obj.gameObject.SetActive(true);
            return obj;
        }

        // Return object to the queue. Usually called from the component that implements the IObjectPoolItem interface
        public override void ReturnObject(Component comp)
        {
            if (comp is not T compObj)
                return;
            
            compObj.gameObject.SetActive(false);
            pool.Enqueue(compObj);
        }

        /*
         * Call when ObjectPool is no longer needed. Destroys all inactive object and releases active ones. Releases active objects
         * should destroy self when it attempts to return to pool
         */
        public override void Release()
        {
            foreach (var item in pool)
            {
                allItems.Remove(item as IObjectPoolItem);
                Object.Destroy(item.gameObject);
            }

            foreach (var item in allItems)
            {
                item.Release();
            }
        }
    }
}