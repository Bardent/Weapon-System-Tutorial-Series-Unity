using Bardent.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    /*
     * The Graphics projectile component is responsible for changing out the sprite on the sprite renderer on the Graphics child GameObject
     * based on what is passed to it from the weapon
     */
    public class Graphics : ProjectileComponent
    {
        private Sprite sprite;

        private SpriteRenderer spriteRenderer;

        protected override void Init()
        {
            base.Init();
            
            spriteRenderer.sprite = sprite;
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not SpriteDataPackage spriteDataPackage)
                return;

            sprite = spriteDataPackage.Sprite;
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        #endregion
    }
}