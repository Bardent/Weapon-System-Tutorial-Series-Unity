using System;
using UnityEngine;

namespace Bardent.Utilities
{
    public class TimeNotifier
    {
        public event Action OnNotify;
        
        private float startTime;
        private float duration;
        private float targetTime;

        private bool isActive;
        
        public TimeNotifier(float duration)
        {
            this.duration = duration;
        }

        public void StartTimer()
        {
            startTime = Time.time;
            targetTime = startTime + duration;
            isActive = true;
        }

        public void StopTimer()
        {
            isActive = false;
        }

        public void Tick()
        {
            if(!isActive) return;

            if (Time.time >= targetTime)
            {
                OnNotify?.Invoke();
                StopTimer();
            }
        }
    }
}