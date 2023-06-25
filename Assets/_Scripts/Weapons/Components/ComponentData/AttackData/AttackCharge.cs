using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackCharge : AttackData
    {
        // How long it takes for a single charge
        [field: SerializeField] public float ChargeTime { get; private set; }
        
        // How many initial charges the component has.
        [field: SerializeField, Range(0, 1)] public int InitialChargeAmount { get; private set; }
        
        // How many times can the attack be charged up
        [field: SerializeField] public int NumberOfCharges { get; private set; }
        
        // Particles that will spawn when we add a charge
        [field: SerializeField] public GameObject ChargeIncreaseIndicatorParticlePrefab { get; private set; }
        
        // Particles that will spawn when we reach full charge
        [field: SerializeField] public GameObject FullyChargedIndicatorParticlePrefab { get; private set; }
        
        
        // Offset relative to the players transform to spawn the particles
        [field: SerializeField] public Vector2 ParticlesOffset { get; private set; }
    }
}