using UnityEngine;

namespace Bardent.CoreSystem
{
    public class Combat : CoreComponent, IKnockbackable
    {
        private Movement Movement
        {
            get => movement ?? core.GetCoreComponent(ref movement);
        }

        private CollisionSenses CollisionSenses
        {
            get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
        }

        private Movement movement;
        private CollisionSenses collisionSenses;


        [SerializeField] private float maxKnockbackTime = 0.2f;

        private bool isKnockbackActive;
        private float knockbackStartTime;

        public override void LogicUpdate()
        {
            CheckKnockback();
        }

        public void Knockback(Vector2 angle, float strength, int direction)
        {
            Movement?.SetVelocity(strength, angle, direction);
            Movement.CanSetVelocity = false;
            isKnockbackActive = true;
            knockbackStartTime = Time.time;
        }

        private void CheckKnockback()
        {
            if (isKnockbackActive
                && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground)
                    || Time.time >= knockbackStartTime + maxKnockbackTime)
               )
            {
                isKnockbackActive = false;
                Movement.CanSetVelocity = true;
            }
        }
    }
}