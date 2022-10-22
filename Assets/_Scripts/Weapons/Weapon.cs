using System;
using UnityEngine;

namespace Bardent.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action OnExit;
        
        private Animator anim;
        private GameObject baseGameObject;
        
        private AnimationEventHandler eventHandler;
        
        public void Enter()
        {
            print($"{transform.name} enter");
            
            anim.SetBool("active", true);
        }

        private void Exit()
        {
            anim.SetBool("active", false);
            
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