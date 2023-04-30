using UnityEngine;

namespace Bardent.Utilities
{
    // NOTE FROM BARDENT: I found this code online here: https://gist.github.com/SolidAlloy/1f87fe7e529a64ba5dc31d0cc82d9a25
    // It helped me debug the boxcast used in the projectile HitBox
    
    /// <summary>
    ///     A class that allows to visualize the Physics2D.BoxCast() method.
    /// </summary>
    /// <remarks>
    ///     Use Draw() to visualize an already cast box,
    ///     and BoxCastAndDraw() to cast a box AND visualize it at the same time.
    /// </remarks>
    public class BoxCastUtilities
    {
        /// <summary>
        ///     Visualizes BoxCast with help of debug lines.
        /// </summary>
        /// <param name="hitInfo"> The cast result. </param>
        /// <param name="origin"> The point in 2D space where the box originates. </param>
        /// <param name="size"> The size of the box. </param>
        /// <param name="angle"> The angle of the box (in degrees). </param>
        /// <param name="direction"> A vector representing the direction of the box. </param>
        /// <param name="distance"> The maximum distance over which to cast the box. </param>
        public static void Draw(
            Vector2 origin,
            Vector2 size,
            float angle,
            Vector2 direction,
            float distance = Mathf.Infinity)
        {
            // Set up points to draw the cast.
            Vector2[] originalBox = CreateOriginalBox(origin, size, angle);

            Vector2 distanceVector = GetDistanceVector(distance, direction);
            Vector2[] shiftedBox = CreateShiftedBox(originalBox, distanceVector);

            // Draw the cast.
            Color castColor = Color.red;
            DrawBox(originalBox, castColor);
            DrawBox(shiftedBox, castColor);
            ConnectBoxes(originalBox, shiftedBox, Color.gray);
        }

        /// <summary>
        ///     Casts a box against colliders in the Scene, returning the first collider to contact with it, and visualizes it.
        /// </summary>
        /// <param name="origin"> The point in 2D space where the box originates. </param>
        /// <param name="size"> The size of the box. </param>
        /// <param name="angle"> The angle of the box (in degrees). </param>
        /// <param name="direction"> A vector representing the direction of the box. </param>
        /// <param name="distance"> The maximum distance over which to cast the box. </param>
        /// <param name="layerMask"> Filter to detect Colliders only on certain layers. </param>
        /// <param name="minDepth"> Only include objects with a Z coordinate (depth) greater than or equal to this value. </param>
        /// <param name="maxDepth"> Only include objects with a Z coordinate (depth) less than or equal to this value. </param>
        /// <returns>
        ///     The cast result.
        /// </returns>
        public static RaycastHit2D[] BoxCastAndDraw(
            Vector2 origin,
            Vector2 size,
            float angle,
            Vector2 direction,
            float distance = Mathf.Infinity,
            int layerMask = Physics2D.AllLayers,
            float minDepth = -Mathf.Infinity,
            float maxDepth = Mathf.Infinity)
        {
            var hitInfo = Physics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth);
            Draw(origin, size, angle, direction, distance);
            return hitInfo;
        }

        private static Vector2[] CreateOriginalBox(Vector2 origin, Vector2 size, float angle)
        {
            float w = size.x * 0.5f;
            float h = size.y * 0.5f;
            Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

            var box = new Vector2[4]
            {
                new Vector2(-w, h),
                new Vector2(w, h),
                new Vector2(w, -h),
                new Vector2(-w, -h),
            };

            for (int i = 0; i < 4; i++)
            {
                box[i] = (Vector2)(q * box[i]) + origin;
            }

            return box;
        }

        private static Vector2[] CreateShiftedBox(Vector2[] box, Vector2 distance)
        {
            var shiftedBox = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                shiftedBox[i] = box[i] + distance;
            }

            return shiftedBox;
        }

        private static void DrawBox(Vector2[] box, Color color)
        {
            Debug.DrawLine(box[0], box[1], color);
            Debug.DrawLine(box[1], box[2], color);
            Debug.DrawLine(box[2], box[3], color);
            Debug.DrawLine(box[3], box[0], color);
        }

        private static void ConnectBoxes(Vector2[] firstBox, Vector2[] secondBox, Color color)
        {
            Debug.DrawLine(firstBox[0], secondBox[0], color);
            Debug.DrawLine(firstBox[1], secondBox[1], color);
            Debug.DrawLine(firstBox[2], secondBox[2], color);
            Debug.DrawLine(firstBox[3], secondBox[3], color);
        }

        private static Vector2 GetDistanceVector(float distance, Vector2 direction)
        {
            if (distance == Mathf.Infinity)
            {
                // Draw some large distance e.g. 5 scene widths long.
                float sceneWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
                distance = sceneWidth * 5f;
            }

            return direction.normalized * distance;
        }
    }
}