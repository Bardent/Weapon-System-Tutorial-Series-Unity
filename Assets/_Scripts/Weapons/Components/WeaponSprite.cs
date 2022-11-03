using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        [SerializeField] private WeaponSprites[] weaponSprites;

        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weaponSpriteRenderer;

        private int currentWeaponSpriteIndex;

        protected override void Awake()
        {
            base.Awake();

            baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

            // TODO: Change back to these version later when weapon data is set up :)
            // baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            // weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        private void HandleEnter()
        {
            currentWeaponSpriteIndex = 0;
        }

        private void OnBaseSpriteChange(SpriteRenderer spriteRenderer)
        {
            weaponSpriteRenderer.sprite = weaponSprites[weapon.CurrentAttackCounter].Sprites[currentWeaponSpriteIndex];

            currentWeaponSpriteIndex++;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            baseSpriteRenderer.RegisterSpriteChangeCallback(OnBaseSpriteChange);

            weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            baseSpriteRenderer.UnregisterSpriteChangeCallback(OnBaseSpriteChange);

            weapon.OnEnter -= HandleEnter;
        }
    }

    [Serializable]
    public class WeaponSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}