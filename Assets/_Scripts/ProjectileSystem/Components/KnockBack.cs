using Bardent.ProjectileSystem.DataPackages;
using Bardent.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Bardent.ProjectileSystem.Components
{
    /*
     * The KnockBack component is responsible for using information provided by the HitBox component via an event
     * to knock back any entities that are detected and on the same layer we are interested in. The knock back information
     * like strength and angle come from the weapon via the ProjectileDataPackage system
     */
    public class KnockBack : ProjectileComponent
    {
        public UnityEvent OnKnockBack;

        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private HitBox hitBox;

        private int direction;
        private float strength;
        private Vector2 angle;

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (!Active)
                return;

            direction = (int)Mathf.Sign(transform.right.x);
            
            foreach (var hit in hits)
            {
                // Is the object under consideration part of the LayerMask that we can damage?
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;

                // NOTE: We need to use .collider.transform instead of just .transform to get the GameObject the collider we detected is attached to, otherwise it returns the parent
                if (!hit.collider.transform.gameObject.TryGetComponent(out IKnockBackable knockBackable))
                    continue;

                knockBackable.KnockBack(angle, strength, direction);

                OnKnockBack?.Invoke();
                
                return;
            }
        }

        // Handles checking to see if the data is relevant or not, and if so, extracts the information we care about
        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not KnockBackDataPackage knockBackDataPackage)
                return;

            strength = knockBackDataPackage.Strength;
            angle = knockBackDataPackage.Angle;
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