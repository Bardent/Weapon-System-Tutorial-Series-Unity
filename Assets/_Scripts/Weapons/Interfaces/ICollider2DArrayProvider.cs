using System;
using UnityEngine;

namespace Bardent.Weapons.Interfaces
{
    public interface ICollider2DArrayProvider
    {
        public event Action<Collider2D[]> OnDetectCollider2D;
    }
}