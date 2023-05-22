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
        
        /// <summary>
        /// This trigger is used to indicate in the weapon animation when the input should be "used" meaning the player has to release the input key and press it down again to trigger the next attack.
        /// Generally this animation event is added to the first "action" frame of an animation. e.g the first sword strike frame, or the frame where the bow is released.
        /// </summary>
        public event Action OnUseInput;

        public event Action<bool> OnSetOptionalSpriteActive;
        
        public event Action<AttackPhases> OnEnterAttackPhase; 

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();
        private void UseInputTrigger() => OnUseInput?.Invoke();

        private void SetOptionalSpriteEnabled() => OnSetOptionalSpriteActive?.Invoke(true);
        private void SetOptionalSpriteDisabled() => OnSetOptionalSpriteActive?.Invoke(false);
        
        private void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase);
    }
}