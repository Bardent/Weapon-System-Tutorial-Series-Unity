using System;
using UnityEngine;

namespace Bardent.ProjectileSystem.DataPackages
{
    /*
    * DamageDataPackage is the data related to the amount of damage the projectile does. If a component is interested in this data they can check to see
    * if the data is of type DamageDataPackage. E.g: if(dataPackage is DamageDataPackage damageDataPackage). See the Damage component for an example
    */
    [Serializable]
    public class DamageDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Amount { get; private set; } 
    }
}