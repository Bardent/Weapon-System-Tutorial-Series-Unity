using System;
using UnityEngine;
using Bardent.CoreSystem;
using Bardent.Weapons.Components;

namespace Bardent.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        protected bool isAttackActive;

        protected AnimationEventHandler EventHandler => weapon.EventHandler ? weapon.EventHandler : GetComponentInChildren<AnimationEventHandler>();
        protected Core Core => weapon.Core;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnEnable()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    public abstract class WeaponComponent<T, U> : WeaponComponent where T : ComponentData<U> where U : AttackData
    {
        protected T data;
        protected U currentAttackData;

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
        }
        
        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.GetData<T>();
        }
    }
}