using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class MovementData : ComponentData<AttackMovement>
    {

        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Movement);
        }
    }
}