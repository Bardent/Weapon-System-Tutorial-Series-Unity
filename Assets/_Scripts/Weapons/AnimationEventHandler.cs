using System;
using UnityEngine;

namespace Bardent.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        private event Action OnFinish; 

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    }
}