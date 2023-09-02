using System;
using UnityEngine;

namespace Bardent
{
    public class Bobber : MonoBehaviour
    {
        [SerializeField] private float yOffset;
        [SerializeField] private float bobDuration;
        [SerializeField] private float stopMultiplier;

        [SerializeField] private AnimationCurve bobCurve;

        private float t;

        private bool isBobbing;
        private bool shouldStopBobbing;

        private Vector3 initialPosition;
        private Vector3 currentPosition;

        public void StartBobbing()
        {
            isBobbing = true;
            shouldStopBobbing = false;
            t = 0f;
        }

        public void StopBobbing()
        {
            shouldStopBobbing = true;
        }

        private void Update()
        {
            if (!isBobbing)
                return;

            if (shouldStopBobbing && t <= 0f)
            {
                isBobbing = false;
                transform.localPosition = initialPosition;
            }

            var curveValue = bobCurve.Evaluate(t / bobDuration);

            currentPosition = transform.localPosition;
            currentPosition.y = initialPosition.y + (yOffset * curveValue);

            transform.localPosition = currentPosition;

            if (!shouldStopBobbing)
            {
                t += Time.deltaTime;
                t %= bobDuration;
            }
            else
            {
                if (t > bobDuration / 2f)
                {
                    t = bobDuration - t;
                }
                
                t -= (Time.deltaTime * stopMultiplier);
            }
        }

        private void Awake()
        {
            initialPosition = transform.localPosition;
        }
    }
}