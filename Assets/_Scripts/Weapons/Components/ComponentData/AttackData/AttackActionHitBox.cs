using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox : AttackData
    {
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}