using System;
using System.Collections;
using Bardent.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Bardent.ProjectileSystem.Components
{
    /// <summary>
    /// This component is responsible for ensuring the projectile gets stuck in a specific layer based on what the HitBox detects
    /// </summary>
    [RequireComponent(typeof(HitBox))]
    public class StickToLayer : ProjectileComponent
    {
        /*
         * Unity events are used to facilitate building some logic in the editor. For example: Setting the Damage Component to Inactive
         * when stuck, and active again when unstuck
         */
        [SerializeField] public UnityEvent setStuck;
        [SerializeField] public UnityEvent setUnstuck;

        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        // SpriteRenderer sorting to be used when projectile is stuck
        [field: SerializeField] public string InactiveSortingLayerName { get; private set; }
        [field: SerializeField] public float CheckDistance { get; private set; }

        private bool isStuck;
        private bool subscribedToDisableNotifier;

        private HitBox hitBox;

        private string activeSortingLayerName;

        private SpriteRenderer sr;

        private OnDisableNotifier onDisableNotifier;

        private Transform referenceTransform;
        private Transform _transform;

        private Vector3 offsetPosition;
        private Quaternion offsetRotation;

        private float gravityScale;

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (isStuck)
                return;

            SetStuck();

            // The point returned by the boxcast can be weird, so we do one last check by firing a ray from the origin to the right to find
            // a more suitable resting point
            var lineHit = Physics2D.Raycast(_transform.position, _transform.right, CheckDistance, LayerMask);

            //If out line hit finds a collider to use, then use it.
            if (lineHit)
            {
                SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
                return;
            }

            // Otherwise look through the hits from the HitBox
            foreach (var hit in hits)
            {
                //HitBox might detect things on more layers than we care about so,
                //did this hit happen with the correct LayerMask we are interested in?
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;

                SetReferenceTransformAndPoint(hit.transform, hit.point);
                return;
            }

            // If there is nothing to get stuck in, set isStuck to false and make body dynamic again so it will fall
            SetUnstuck();
        }

        // Set projectile position to point and set new reference transform for projectile to track
        private void SetReferenceTransformAndPoint(Transform newReferenceTransform, Vector2 newPoint)
        {
            if (newReferenceTransform.TryGetComponent(out onDisableNotifier))
            {
                onDisableNotifier.OnDisableEvent += HandleDisableNotifier;
                subscribedToDisableNotifier = true;
            }

            // Set projectile position to detected point
            _transform.position = newPoint;

            // Set reference transform and cache position and rotation offset
            referenceTransform = newReferenceTransform;
            offsetPosition = _transform.position - referenceTransform.position;
            offsetRotation = Quaternion.Inverse(referenceTransform.rotation) * _transform.rotation;
        }

        // Set Rigidbody2D bodyType to static so that it is not affected by gravity and set sorting layer such that projectile appears behind other items
        private void SetStuck()
        {
            isStuck = true;

            sr.sortingLayerName = InactiveSortingLayerName;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;

            setStuck?.Invoke();
        }

        // Set Rigidbody2D bodyType to dynamic so that it is affected by gravity again and set sorting layer such that projectile appears in front of other items
        private void SetUnstuck()
        {
            isStuck = false;

            sr.sortingLayerName = activeSortingLayerName;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = gravityScale;

            setUnstuck?.Invoke();
        }

        // If the body we are stuck in gets disabled or destroyed, make projectile dynamic again
        private void HandleDisableNotifier()
        {
            SetUnstuck();

            if (!subscribedToDisableNotifier)
                return;

            onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            subscribedToDisableNotifier = false;
        }

        protected override void ResetProjectile()
        {
            base.ResetProjectile();

            SetUnstuck();
        }


        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            gravityScale = rb.gravityScale;

            _transform = transform;

            sr = GetComponentInChildren<SpriteRenderer>();
            activeSortingLayerName = sr.sortingLayerName;

            hitBox = GetComponent<HitBox>();

            hitBox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
        }

        protected override void Update()
        {
            base.Update();

            if (!isStuck)
                return;

            // Update position and rotation based on reference transform
            if (!referenceTransform)
            {
                SetUnstuck();
                return;
            }

            var referenceRotation = referenceTransform.rotation;
            _transform.position = referenceTransform.position + referenceRotation * offsetPosition;
            _transform.rotation = referenceRotation * offsetRotation;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit2D.RemoveListener(HandleRaycastHit2D);

            if (subscribedToDisableNotifier)
            {
                onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            }
        }

        #endregion
    }
}