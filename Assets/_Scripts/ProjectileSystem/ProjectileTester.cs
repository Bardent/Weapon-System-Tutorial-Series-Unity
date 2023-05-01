using System;
using Bardent.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Bardent.ProjectileSystem
{
    /*
     * Before the projectiles are fully integrated with the rest of the weapon system, we need to develop and test them. Doing this in a separate scene makes it easier
     * to make sure everything is working as expected before moving on.
     */
    public class ProjectileTester : MonoBehaviour
    {
        public Projectile Projectile;

        public DamageDataPackage DamageDataPackage;

        private void Start()
        {
            if (!Projectile)
            {
                Debug.LogWarning("No Projectile To Test");
                return;
            }
            
            Projectile.SendDataPackage(DamageDataPackage);
            
            Projectile.Init();
        }
    }
}