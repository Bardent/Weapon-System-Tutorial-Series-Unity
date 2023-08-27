using System;
using Bardent.Combat.Parry;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class ParryReceiver : CoreComponent, IParryable
    {
        public event Action OnParried;
        
        private Collider2D parryCollider;

        public void Parry(ParryData data)
        {
            OnParried?.Invoke();
        }
        
        public void SetParryColliderActive(bool value)
        {
            parryCollider.enabled = value;
        }
        
        protected override void Awake()
        {
            base.Awake();

            parryCollider = GetComponent<Collider2D>();
            parryCollider.enabled = false;
        }

    }
}