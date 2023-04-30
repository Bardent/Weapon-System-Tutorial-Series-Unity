using System;
using Bardent.CoreSystem;
using UnityEngine;
using Bardent.Utilities;

namespace Bardent.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action<bool> OnCurrentInputChange;
        
        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnUseInput;
        
        [SerializeField] private float attackCounterResetCooldown;

        public WeaponDataSO Data { get; private set; }
        
        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value; 
        }

        public bool CurrentInput
        {
            get => currentInput;
            set
            {
                if (currentInput != value)
                {
                    currentInput = value;
                    OnCurrentInputChange?.Invoke(currentInput);
                }
            }
        }

        
        private Animator anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }
        
        public AnimationEventHandler EventHandler { get; private set; }
        
        public Core Core { get; private set; }

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;

        private bool currentInput;
        
        public void Enter()
        {            
            print($"{transform.name} enter");
            
            attackCounterResetTimer.StopTimer();
            
            anim.SetBool("active", true);
            anim.SetInteger("counter", currentAttackCounter);
            
            OnEnter?.Invoke();
        }

        public void SetCore(Core core)
        {
            Core = core;
        }

        public void SetData(WeaponDataSO data)
        {
            Data = data;
        }

        private void Exit()
        {
            anim.SetBool("active", false);

            CurrentAttackCounter++;
            attackCounterResetTimer.StartTimer();
            
            OnExit?.Invoke();
        }

        private void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            
            anim = BaseGameObject.GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void ResetAttackCounter()
        {
            print("Reset Attack Counter");
            CurrentAttackCounter = 0;
        }

        private void OnEnable()
        {
            EventHandler.OnFinish += Exit;
            EventHandler.OnUseInput += HandleUseInput;
            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= Exit;
            EventHandler.OnUseInput -= HandleUseInput;
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }

        /// <summary>
        /// Invokes event to pass along information from the AnimationEventHandler to a non-weapon class.
        /// </summary>
        private void HandleUseInput() => OnUseInput?.Invoke();
    }
}