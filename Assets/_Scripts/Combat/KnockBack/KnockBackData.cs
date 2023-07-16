using UnityEngine;

namespace Bardent.Combat.KnockBack
{
    public class KnockBackData
    {
        public Vector2 Angle;
        public float Strength;
        public int Direction;
        public GameObject Source { get; private set; }

        public KnockBackData(Vector2 angle, float strength, int direction, GameObject source)
        {
            Angle = angle;
            Strength = strength;
            Direction = direction;
            Source = source;
        }
    }
}