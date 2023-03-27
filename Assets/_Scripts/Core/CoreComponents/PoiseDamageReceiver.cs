using System;
using Bardent.Interfaces;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {
        private Stats stats;

        public void DamagePoise(float amount)
        {
               
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
        }
    }
}