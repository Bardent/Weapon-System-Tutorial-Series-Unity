using System;
using UnityEngine;

namespace Bardent.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField] public float MaxValue { get; private set; }

        public event Action OnCurrentValueZero;

        public float CurrentValue
        {
            get => currentValue;
            private set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);

                if (currentValue <= 0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        } 

        private float currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;
    }
}