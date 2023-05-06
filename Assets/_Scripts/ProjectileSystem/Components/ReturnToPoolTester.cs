using System;
using System.Collections;
using Bardent.Interfaces;
using Bardent.ObjectPoolSystem;
using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    /*
     * Temporary tester component to test the object pool system.
     */
    public class ReturnToPoolTester : ProjectileComponent, IObjectPoolItem
    {
        // Reference to the pool this object would belong to
        private ObjectPool<Projectile> objectPool;
        
        protected override void Init()
        {
            base.Init();

            // Return to pool after x amount of time
            StartCoroutine(ReturnToPool());
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(1f);

            // If pool reference is set, return to pool
            if (objectPool != null)
            {
                objectPool.ReturnObject(projectile);
            }
            // Otherwise, destroy
            else
            {
                Destroy(gameObject);
            }
        }

        // Implement the IObjectPoolItem interface. Might be worth just making this a reusable component
        public void SetObjectPool<T>(ObjectPool<T> pool) where T : Component
        {
            objectPool = pool as ObjectPool<Projectile>;
        }

        public void Release()
        {
            objectPool = null;
        }
    }
}