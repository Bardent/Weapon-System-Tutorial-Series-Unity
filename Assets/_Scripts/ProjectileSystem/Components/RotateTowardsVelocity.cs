using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    /// <summary>
    /// This class rotates the current GameObject such that transform.Right points in the same direction as the velocity vector
    /// </summary>
    public class RotateTowardsVelocity : ProjectileComponent
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            var velocity = rb.velocity;

            if (velocity.Equals(Vector3.zero))
                return;

            // Find velocity vector angle
            var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            
            // Apply angle as rotation around Vector3.forward (So using the vector pointing in to your screen as the axis around which we are rotating)
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}