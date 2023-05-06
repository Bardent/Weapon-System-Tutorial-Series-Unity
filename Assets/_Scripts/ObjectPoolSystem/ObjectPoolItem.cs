using System;
using System.Collections;
using System.Collections.Generic;
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

        // Other components can call this to return object to pool, either immediately or with a delay
        public void ReturnItem(float delay = 0f)
        {
            if (delay > 0)
            {
                StartCoroutine(ReturnItemWithDelay(delay));
                return;
            }
            
            ReturnItemToPool();
        }

        private void ReturnItemToPool()
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

        private IEnumerator ReturnItemWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            ReturnItemToPool();
        }

        public void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component
        {
            objectPool = pool;
            
            // Reference the object that the pool is actually interested in so we can return it
            component = GetComponent(comp.GetType());
        }

        public void Release()
        {
            objectPool = null;
        }
    }
}