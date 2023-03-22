using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class AttackKnockBack : AttackData
    {
        [field: SerializeField] public Vector2 KnockBackAngle { get; private set; }
        [field: SerializeField] public float KnockBackStrength { get; private set;}
    }
}