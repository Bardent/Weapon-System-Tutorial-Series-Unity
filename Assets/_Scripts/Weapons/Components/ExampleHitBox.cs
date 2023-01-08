using System;
using Bardent.Weapons.Interfaces;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ExampleHitBox : WeaponComponent<ExampleHitBoxData, AttackExampleHitBox>, ICollider2DArrayProvider
    {
        public event Action<Collider2D[]> OnDetectCollider2D;

        private Collider2D[] cols;
        
        protected override void HandleEnter()
        {
            base.HandleEnter();
            cols = new Collider2D[5];
            
            OnDetectCollider2D?.Invoke(cols);
        }
    }
}