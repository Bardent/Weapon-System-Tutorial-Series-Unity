using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class WeaponSpriteData : ComponentData<AttackSprites>
    {
        public WeaponSpriteData()
        {
            dependency = typeof(WeaponSprite);
        }
    }
}