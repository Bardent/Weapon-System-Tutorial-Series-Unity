using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector] private string name;
        
        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }
        
        public void SetComponentName() => name = GetType().Name;

        protected abstract void SetComponentDependency();

        public virtual void SetAttackDataNames(){}
        
        public virtual void InitializeAttackData(int numberOfAttacks){}
    }

    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {
        [SerializeField] private bool repeatData;
        [SerializeField] private T[] attackData;

        // Use this to get the data of a specific attack. Accounts for components that repeats data for all attacks.
        public T GetAttackData(int index) => attackData[repeatData ? 0 : index];

        public T[] GetAllAttackData() => attackData;
        
        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();
            
            for (var i = 0; i < attackData.Length; i++)
            {
                attackData[i].SetAttackName(i + 1);
            }
        }
        
        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            var newLen = repeatData ? 1 : numberOfAttacks;
            
            var oldLen = attackData != null ? attackData.Length : 0;
            
            if(oldLen == newLen)
                return;
            
            Array.Resize(ref attackData, newLen);

            if (oldLen < newLen)
            {
                for (var i = oldLen; i < attackData.Length; i++)
                {
                    var newObj = Activator.CreateInstance(typeof(T)) as T;
                    attackData[i] = newObj;
                }
            }
            
            SetAttackDataNames();
        }
    }
}