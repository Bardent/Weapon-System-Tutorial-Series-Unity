using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.ProjectileSystem.Components;
using Bardent.ProjectileSystem.DataPackages;
using Bardent.Utilities;
using UnityEngine;

namespace Bardent.ProjectileSystem
{
    /*
     * This component is responsible for rotating a projectile such that it points in the direction of a target. The projectile is provided with a list of targets
     * and the closest one is chosen. We might need to update how the target is chosen to make more sense. We also lerp how quickly the projectile can rotate so that we
     * can give our projectiles more of a curve
     */
    public class DirectTowardsTarget : ProjectileComponent
    {
        [SerializeField] private float minStep;
        [SerializeField] private float maxStep;
        [SerializeField] private float timeToMaxStep;

        private List<Transform> targets;
        private Transform currentTarget;

        private float step;
        private float startTime;

        private Vector2 direction;

        protected override void Init()
        {
            base.Init();

            currentTarget = null;

            startTime = Time.time;

            step = minStep;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!HasTarget())
                return;

            step = Mathf.Lerp(minStep, maxStep, (Time.time - startTime) / timeToMaxStep);
            direction = (currentTarget.position - transform.position).normalized;

            Rotate(direction);
        }

        private bool HasTarget()
        {
            if (currentTarget)
                return true;

            targets.RemoveAll(item => item == null);

            if (targets.Count <= 0)
                return false;

            targets = targets.OrderBy(target => (target.position - transform.position).sqrMagnitude).ToList();
            currentTarget = targets[0];
            return true;
        }

        private void Rotate(Vector2 dir)
        {
            if (dir.Equals(Vector2.zero))
                return;

            var toRotation = QuaternionExtensions.Vector2ToRotation(dir);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, step * Time.deltaTime);
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not TargetsDataPackage targetsDataPackage)
                return;

            targets = targetsDataPackage.targets;
        }

        private void OnDrawGizmos()
        {
            if (!currentTarget)
                return;

            Gizmos.DrawLine(transform.position, currentTarget.position);
        }
    }
}