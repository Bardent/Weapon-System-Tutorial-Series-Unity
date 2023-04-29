using Bardent.Utilities;
using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    /// <summary>
    /// This component is responsible for ensuring the projectile gets stuck in a specific layer based on what the HitBox detects
    /// </summary>
    [RequireComponent(typeof(HitBox))]
    public class StickToLayer : ProjectileComponent
    {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public float CheckDistance { get; private set; }

        private bool isStuck;

        private HitBox hitBox;

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (isStuck)
                return;

            // The point returned by the boxcast can be weird, so we do one last check by firing a ray from the origin to the right to find
            // a more suitable resting point
            var lineHit = Physics2D.Raycast(transform.position, transform.right, CheckDistance, LayerMask);

            // Remove velocity and set body to static so that it is not affected by other things. This might have to be adjusted
            // depending on the behavior you want
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;

            isStuck = true;

            //If out line hit finds a collider to use, then use it.
            if (lineHit)
            {
                transform.position = lineHit.point;
                return;
            }

            // Otherwise look through the hits from the HitBox
            foreach (var hit in hits)
            {
                //HitBox might detect things on more layers than we care about so,
                //did this hit happen with the correct LayerMask we are interested in?
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;

                transform.position = hit.point;
                return;
            }

            // If there is nothing to get stuck in, set isStuck to false and make body dynamic again so it will fall
            isStuck = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        protected override void Init()
        {
            base.Init();

            isStuck = false;
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<HitBox>();

            hitBox.OnRaycastHit2D += HandleRaycastHit2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit2D -= HandleRaycastHit2D;
        }

        #endregion
    }
}