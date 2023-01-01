using System;
using UnityEngine;

namespace Bardent.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnAttackAction;
        
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
    }
}