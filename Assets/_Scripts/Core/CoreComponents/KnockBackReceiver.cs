using UnityEngine;

namespace Bardent.CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockbackable
    {
        [SerializeField] private float maxKnockBackTime = 0.2f;

        private Movement movement;
        private CollisionSenses collisionSenses;

        private bool isKnockBackActive;
        private float knockBackStartTime;

        public override void LogicUpdate()
        {
            CheckKnockBack();
        }

        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            movement?.SetVelocity(strength, angle, direction);
            movement.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }

        private void CheckKnockBack()
        {
            if (
                isKnockBackActive &&
                ((movement?.CurrentVelocity.y <= 0.01f && collisionSenses.Ground) ||
                 Time.time >= knockBackStartTime + maxKnockBackTime)
            )
            {
                isKnockBackActive = false;
                movement.CanSetVelocity = true;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            movement = new CoreComp<Movement>(core);
            collisionSenses = new CoreComp<CollisionSenses>(core);
        }
    }
}