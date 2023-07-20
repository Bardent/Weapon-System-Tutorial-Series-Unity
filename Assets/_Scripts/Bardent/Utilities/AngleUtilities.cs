using UnityEngine;

namespace Bardent.Utilities
{
    public static class AngleUtilities
    {
        /*
         * Calculates the angle of a line created by two transforms from the positive or negative x-axis
         */
        public static float AngleFromFacingDirection(Transform receiver, Transform source, int direction)
        {
            return Vector2.SignedAngle(Vector2.right * direction,
                source.position - receiver.position) * direction;
        }
    }
}