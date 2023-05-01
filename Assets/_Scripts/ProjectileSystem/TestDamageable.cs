using System;
using Bardent.ProjectileSystem.Components;
using UnityEngine;

namespace Bardent.ProjectileSystem
{
    /*
     * This MonoBehaviour is simply used to print the damage amount received in the ProjectileTestScene
     */
    public class TestDamageable : MonoBehaviour, IDamageable
    {
        public void Damage(float amount)
        {
            print($"{gameObject.name} Damaged: {amount}");
        }
    }
}