using System;
using Bardent.Weapons.Components.Data;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weaponSpriteRenderer;

        private WeaponSpriteData data;
        
        private int currentWeaponSpriteIndex;

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            currentWeaponSpriteIndex = 0;
        }

        private void HandleBaseSpriteChange(SpriteRenderer sr)
        {
            if (!isAttackActive)
            {
                weaponSpriteRenderer.sprite = null;
                return;
            }

            var currentAttackSprites = data.AttackData[weapon.CurrentAttackCounter].Sprites;

            if (currentWeaponSpriteIndex >= currentAttackSprites.Length)
            {
                Debug.LogWarning($"{weapon.name} weapon sprites length mismatch");
                return;
            }
            
            weaponSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];

            currentWeaponSpriteIndex++;
        }

        protected override void Awake()
        {
            base.Awake();

            baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

            data = weapon.WeaponData.GetData<WeaponSpriteData>();

            // TODO: Fix this when we create weapon data
            // baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            // weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

            weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);

            weapon.OnEnter -= HandleEnter;
        }
    }
}