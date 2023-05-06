using System;
using System.Collections;
using Bardent.Interfaces;
using Bardent.ObjectPoolSystem;
using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    public class ReturnToPoolTester : ProjectileComponent, IObjectPoolItem
    {
        private ObjectPool<Projectile> objectPool;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Init()
        {
            base.Init();
            
            StartCoroutine(ReturnToPool());
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(1f);
            objectPool.ReturnObject(projectile);
        }

        public void SetObjectPool<T>(ObjectPool<T> pool) where T : Component
        {
            objectPool = pool as ObjectPool<Projectile>;
        }
    }
}