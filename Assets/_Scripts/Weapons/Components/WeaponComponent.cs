using System;
using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected bool isInitialized;
        
        protected Weapon weapon;

        protected AnimationEventHandler EventHandler => weapon.EventHandler;
        
        protected Core Core => weapon.Core;

        protected bool isAttackActive;

        public virtual void Init()
        {
            isInitialized = true;
            SubscribeHandlers();
        }
        
        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
        }
        
        protected virtual void Start(){}

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void SubscribeHandlers()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnEnable()
        {
            if(!isInitialized) return;
            
            SubscribeHandlers();
        }

        protected virtual void OnDisable()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
        }

        public override void Init()
        {
            base.Init();

            data = weapon.Data.GetData<T1>();
        }
    }
}