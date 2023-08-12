using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class ParryData : ComponentData<AttackParry>
    {
        [field: SerializeField] public LayerMask ParryableLayers;
        
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Parry);
        }
    }
}