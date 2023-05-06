using Bardent.ObjectPoolSystem;
using UnityEngine;

namespace Bardent.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool<T> pool) where T : Component;

        void Release();
    }
}