using System;
using UnityEngine;

namespace Bardent.Utilities
{
    /// <summary>
    /// Distance notifier takes in a starting position and a desired distance from that position. When an object
    /// reaches that distance from the target, it invokes an event.
    /// </summary>
    public class DistanceNotifier
    {
        /// <summary>
        /// This event is broadcast whenever the distance condition is met. If checkInside is true then event will be broadcast if the current distance
        /// is less than distance, otherwise it will broadcast if it is greater. It will also broadcast continuously while enabled is true
        /// </summary>
        public event Action OnNotify;

        private Vector3 referencePos;
        private float distance;
        private float sqrDistance;

        // This boolean determines if we fire off the event when we are closer than distance (true) or further than distance (false)
        private bool checkInside;
        private bool enabled;

        // Initializes the notifier with a new reference position and distance
        public void Init(Vector3 referencePos, float distance, bool checkInside = false,
            bool triggerContinuously = false)
        {
            this.referencePos = referencePos;
            this.distance = distance;

            sqrDistance = distance * distance;

            this.checkInside = checkInside;

            enabled = true;

            if (!triggerContinuously)
            {
                //OnNotify will only broadcast once before disabling enabled bool
                OnNotify += Disable;
            }
        }

        // Sets enabled to false to stop more event broadcasts
        public void Disable()
        {
            enabled = false;

            OnNotify -= Disable;
        }

        // Tick current position to compare to reference position
        public void Tick(Vector3 pos)
        {
            if (!enabled)
                return;

            // We are using the square of distances as square root function is expensive
            var currentSqrDistance = (referencePos - pos).sqrMagnitude;

            if (checkInside)
            {
                CheckInside(currentSqrDistance);
            }
            else
            {
                CheckOutside(currentSqrDistance);
            }
        }

        private void CheckInside(float dist)
        {
            if (dist <= sqrDistance)
            {
                OnNotify?.Invoke();
            }
        }

        private void CheckOutside(float dist)
        {
            if (dist >= sqrDistance)
            {
                OnNotify?.Invoke();
            }
        }
    }
}