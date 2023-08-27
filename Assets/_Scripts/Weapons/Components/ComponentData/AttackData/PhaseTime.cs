using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public struct PhaseTime
    {
        [field: SerializeField] public float Duration { get; private set; }

        [field: SerializeField] public AttackPhases Phase { get; private set; }
        
        public bool TryGetTriggerTime(AttackPhases phase, out float triggerTime)
        {
            triggerTime = Time.time + Duration;
            return phase == Phase;
        }
    }
}