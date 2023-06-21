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
        
        private bool enabled;

        // Action to hold function that determines if we notify when we are inside or outside of the distance
        private Action<float> notifierCondition;

        // Initializes the notifier with a new reference position and distance
        public void Init(Vector3 referencePos, float distance, bool checkInside = false,
            bool triggerContinuously = false)
        {
            this.referencePos = referencePos;
            this.distance = distance;

            sqrDistance = distance * distance;

            // Store relevant function in Action.
            if (checkInside)
            {
                notifierCondition = CheckInside;
            }
            else
            {
                notifierCondition = CheckOutside;
            }

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
            
            // Pass current distance to function stored within the Action. Avoids having to do an if else check every tick and instead moves that check to constructor.
            notifierCondition.Invoke(currentSqrDistance);
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