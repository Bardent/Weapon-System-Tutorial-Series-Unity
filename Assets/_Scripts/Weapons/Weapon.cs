using System;
using Bardent.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bardent.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int numberOfAttacks;
        [SerializeField] private float attackCounterResetCooldown;

        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= numberOfAttacks ? 0 : value;
        }
        
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public event Action OnEnter;
        public event Action OnExit;
        
        private Animator anim;
        
        private AnimationEventHandler eventHandler;

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;

        private GameObject baseGameObject;
        private GameObject weaponSpriteGameObject;
        
        private static readonly int Active = Animator.StringToHash("active");
        private static readonly int Counter = Animator.StringToHash("counter");

        public void Enter()
        {
            attackCounterResetTimer.StopTimer();
            
            anim.SetBool(Active, true);
            anim.SetInteger(Counter, currentAttackCounter);
            
            OnEnter?.Invoke();
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void Exit()
        {
            OnExit?.Invoke();
            
            anim.SetBool(Active, false);

            CurrentAttackCounter++;
            
            attackCounterResetTimer.StartTimer();
        }

        private void ResetAttackCounter()
        {
            CurrentAttackCounter = 0;
        }

        private void Awake()
        {
            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
            
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            
            anim = BaseGameObject.GetComponent<Animator>();

            eventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
        }

        private void OnEnable()
        {
            eventHandler.OnFinish += Exit;

            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            eventHandler.OnFinish -= Exit;
            
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }
    }
}