using System;
using Bardent._Scripts.ScriptableObjects;
using UnityEngine;
using Bardent.Utilities;

namespace Bardent.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponDataSO weaponData;
        
        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= weaponData.NumberOfAttacks ? 0 : value; 
        }

        public event Action OnEnter;
        public event Action OnExit;
        
        private Animator anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }
        
        private AnimationEventHandler eventHandler;

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;
        
        public void Enter()
        {
            print($"{transform.name} enter");
            
            attackCounterResetTimer.StopTimer();
            
            anim.SetBool("active", true);
            anim.SetInteger("counter", currentAttackCounter);
            
            OnEnter?.Invoke();
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

            eventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            attackCounterResetTimer = new Timer(weaponData.AttackCounterResetTimer);
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