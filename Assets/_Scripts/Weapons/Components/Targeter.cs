using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    /*
     * This component detects any colliders on a given Layer every FixedUpdate while the attack is active.
     * Other components can request a list of these targets that are in range at any time.
     */
    public class Targeter : WeaponComponent<TargeterData, AttackTargeter>
    {
        private List<Transform> targets = new List<Transform>();

        private CoreSystem.Movement movement;

        private bool isActive;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            isActive = true;
        }

        protected override void HandleExit()
        {
            base.HandleExit();

            isActive = false;
        }

        public List<Transform> GetTargets()
        {
            return targets;
        }
        
        private void CheckForTargets()
        {
            var pos = transform.position +
                      new Vector3(currentAttackData.Area.center.x * movement.FacingDirection, currentAttackData.Area.center.y);

            var targetColliders =
                Physics2D.OverlapBoxAll(pos, currentAttackData.Area.size, 0f, currentAttackData.DamageableLayer);

            targets = targetColliders.Select(item => item.transform).ToList();
        }
        
        #region Plumbing

        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();
        }

        private void FixedUpdate()
        {
            if(!isActive)
                return;

            CheckForTargets();
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null)
                return;

            foreach (var attackTargeter in data.GetAllAttackData())
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)attackTargeter.Area.center, attackTargeter.Area.size);
            }

            Gizmos.color = Color.red;
            foreach (var target in targets)
            {
                Gizmos.DrawWireSphere(target.position, 0.25f);
            }
        }

        #endregion
    }
}