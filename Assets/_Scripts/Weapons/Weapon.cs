using System;
using UnityEngine;

namespace Bardent.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int NumberOfAttacks;

        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= NumberOfAttacks ? 0 : value;
        }
        
        public event Action OnExit;
        
        private Animator anim;
        private GameObject baseGameObject;
        
        private AnimationEventHandler eventHandler;

        private int currentAttackCounter;

        public void Enter()
        {
            print($"{transform.name} enter");
            
            anim.SetBool("active", true);
            anim.SetInteger("counter", currentAttackCounter);
        }

        private void Exit()
        {
            anim.SetBool("active", false);

            CurrentAttackCounter++;
            
            OnExit?.Invoke();
        }

        private void Awake()
        {
            baseGameObject = transform.Find("Base").gameObject;
            anim = baseGameObject.GetComponent<Animator>();

            eventHandler = baseGameObject.GetComponent<AnimationEventHandler>();
        }

        private void OnEnable()
        {
            eventHandler.OnFinish += Exit;
        }

        private void OnDisable()
        {
            eventHandler.OnFinish -= Exit;
        }
    }
}