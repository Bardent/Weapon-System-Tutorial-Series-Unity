using System;
using Bardent.ObjectPoolSystem;
using UnityEngine;

namespace Bardent.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;

        void Release();
    }
}