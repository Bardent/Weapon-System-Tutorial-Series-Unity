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
    public class ReturnToPoolTester : ProjectileComponent
    {
        public float timeToReturn;
        
        private ObjectPoolItem objectPoolItem;

        protected override void Awake()
        {
            base.Awake();

            objectPoolItem = GetComponent<ObjectPoolItem>();
        }

        protected override void Init()
        {
            base.Init();

            // Return to pool after x amount of time
            StartCoroutine(ReturnToPool());
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(timeToReturn);

            objectPoolItem.ReturnItem();
        }
    }
}