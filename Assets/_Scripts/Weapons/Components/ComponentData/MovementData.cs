using Bardent.Weapons.Components.ComponentData.AttackData;
using UnityEngine;

namespace Bardent.Weapons.Components.ComponentData
{
    public class MovementData : ComponentData
    {
        [field: SerializeField] public AttackMovement[] AttackData { get; private set; }
    }
}