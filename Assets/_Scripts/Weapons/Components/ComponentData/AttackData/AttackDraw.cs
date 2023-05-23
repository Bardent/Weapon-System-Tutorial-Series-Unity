using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackDraw : AttackData
    {
        // Allows us to adjust how the draw curve progresses over time - keep time and value between 0 and 1
        [field: SerializeField] public AnimationCurve DrawCurve { get; private set; }
        
        // The total time it takes to fully draw the bow -- If you want to calculate this based on frames it's drawTime = (1 / animationSampleRate) * numberOfFrames
        [field: SerializeField] public float DrawTime { get; private set; }
    }
}