using System;
using Bardent.Combat.Damage;
using Bardent.ProjectileSystem.Components;
using UnityEngine;

namespace Bardent.ProjectileSystem
{
    /*
     * This MonoBehaviour is simply used to print the damage amount received in the ProjectileTestScene
     */
    public class TestDamageable : MonoBehaviour, IDamageable
    {
        public void Damage(DamageData data)
        {
            print($"{gameObject.name} Damaged: {data.Amount}");
        }
    }
}