using UnityEngine;

namespace Bardent.Weapons.Components.ComponentData.AttackData
{
    public class AttackMovement
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public float Velocity { get; private set; }
    }
}