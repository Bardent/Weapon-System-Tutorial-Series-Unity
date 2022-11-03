using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        [SerializeField] private Sprite[] sprites;

        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weaponSpriteRenderer;

        protected override void Awake()
        {
            base.Awake();

            baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        }
    }
}