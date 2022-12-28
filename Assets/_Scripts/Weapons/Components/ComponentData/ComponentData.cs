using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        public ComponentData()
        {
            SetComponentName();
        }

        public void SetComponentName() => name = GetType().Name;

        public virtual void SetAttackDataNames()
        {
        }
    }

    [Serializable]
    public class ComponentData<T> : ComponentData where T : AttackData
    {
        [field: SerializeField] public T[] AttackData { get; private set; }

        public override void SetAttackDataNames()
        {
            for (var i = 0; i < AttackData.Length; i++)
            {
                AttackData[i].SetAttackName(i + 1);
            }
        }
    }
}