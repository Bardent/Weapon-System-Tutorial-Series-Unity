using Bardent.Weapons.Components.ComponentData.AttackData;
using UnityEngine;

namespace Bardent.Weapons.Components.ComponentData
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; private set; }
    }
}