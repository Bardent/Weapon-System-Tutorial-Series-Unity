using System;
using Bardent.Utilities;
using UnityEngine;

namespace Bardent.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int NumberOfAttacks;
        [SerializeField] private float AttackCounterResetCooldown;

        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= NumberOfAttacks ? 0 : value;
        }
        
        public GameObject BaseGameObject { get; private set; }
        
        public event Action OnExit;
        
        private Animator anim;
        
        private AnimationEventHandler eventHandler;

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;

        public void Enter()
        {
            print($"{transform.name} enter");
            
            attackCounterResetTimer.StopTimer();
            
            anim.SetBool("active", true);
            anim.SetInteger("counter", currentAttackCounter);
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void Exit()
        {
            anim.SetBool("active", false);

            CurrentAttackCounter++;
            
            attackCounterResetTimer.StartTimer();
            
            OnExit?.Invoke();
        }

        public void ResetAttackCounter()
        {
            print("Reset Attack Counter");
            CurrentAttackCounter = 0;
        }

        private void Awake()
        {
            attackCounterResetTimer = new Timer(AttackCounterResetCooldown);
            
            BaseGameObject = transform.Find("Base").gameObject;
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