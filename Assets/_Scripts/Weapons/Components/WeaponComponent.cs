using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        private void Awake()
        {
            weapon = GetComponent<Weapon>();
        }
    }
}