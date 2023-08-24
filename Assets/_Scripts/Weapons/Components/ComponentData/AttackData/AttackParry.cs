using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackParry : AttackData
    {
        [field: SerializeField] public DirectionalInformation[] ParryDirectionalInformation { get; private set; }
        
        /*
         * Checks angle against all parry regions. Also gives back the information for the region that is actually doing the parrying as it has other information
         * like how much damage we actually do block by parrying
         */
        public bool IsBlocked(float angle, out DirectionalInformation directionalInformation)
        {
            directionalInformation = null;

            foreach (var directionInformation in ParryDirectionalInformation)
            {
                var blocked = directionInformation.IsAngleBetween(angle);
                
                if (!blocked)
                    continue;
                
                directionalInformation = directionInformation;
                return true;
            }

            return false;
        }
    }
}