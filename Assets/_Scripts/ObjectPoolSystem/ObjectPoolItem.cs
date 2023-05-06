using System;
using Bardent.Interfaces;
using UnityEngine;

namespace Bardent.ObjectPoolSystem
{
    /*
     * Implementation of the IObjectPoolItem interface that can be used in most cases.
     */
    public class ObjectPoolItem : MonoBehaviour, IObjectPoolItem
    {
        private ObjectPool objectPool;
        private Component component;

        public void ReturnItem()
        {
            // If pool reference is set, return to pool
            if (objectPool != null)
            {
                objectPool.ReturnObject(component);
            }
            // Otherwise, destroy
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component
        {
            objectPool = pool;
            component = GetComponent(comp.GetType());
        }

        public void Release()
        {
            objectPool = null;
        }
    }
}