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
        public event Action OnMinHoldPassed;
        public event Action OnUseInput;
        
        public event Action<AttackPhases> OnEnterAttackPhase; 

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();
        private void UseInputTrigger() => OnUseInput?.Invoke();
        
        private void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase);
    }
}