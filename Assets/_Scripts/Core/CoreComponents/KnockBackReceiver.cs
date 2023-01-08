using UnityEngine;

namespace Bardent.CoreSystem
{
    public class Combat : CoreComponent, IKnockbackable
    {
        [SerializeField] private float maxKnockBackTime = 0.2f;

        private CoreComp<Movement> movement;
        private CoreComp<CollisionSenses> collisionSenses;

        private bool isKnockBackActive;
        private float knockBackStartTime;

        public override void LogicUpdate()
        {
            CheckKnockBack();
        }

        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            movement.Comp?.SetVelocity(strength, angle, direction);
            movement.Comp.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }

        private void CheckKnockBack()
        {
            if (
                isKnockBackActive &&
                ((movement.Comp?.CurrentVelocity.y <= 0.01f && collisionSenses.Comp.Ground) ||
                 Time.time >= knockBackStartTime + maxKnockBackTime)
            )
            {
                isKnockBackActive = false;
                movement.Comp.CanSetVelocity = true;
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