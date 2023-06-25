using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackCharge : AttackData
    {
        [field: SerializeField] public float ChargeTime { get; private set; }
        [field: SerializeField] public int InitialChargeAmount { get; private set; }
        [field: SerializeField] public int NumberOfCharges { get; private set; }
        [field: SerializeField] public GameObject ChargeIncreaseIndicatorParticlePrefab { get; private set; }
        [field: SerializeField] public GameObject FullyChargedIndicatorParticlePrefab { get; private set; }
        [field: SerializeField] public Vector2 Offset { get; private set; }
    }
}