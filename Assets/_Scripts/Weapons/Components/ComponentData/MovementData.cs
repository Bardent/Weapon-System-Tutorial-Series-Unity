using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class MovementData : ComponentData<AttackMovement>
    {
        public MovementData()
        {
            dependency = typeof(Movement);
        }
    }
}