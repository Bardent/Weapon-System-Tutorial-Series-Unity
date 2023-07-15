using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    /*
     * The data for a single block attack. Blocking works by checking the angle of an incoming attack and comparing that to an array
     * of angle regions that can be blocked.
     */
    [Serializable]
    public class AttackBlock : AttackData
    {
        // All blocking regions for a single attack
        [field: SerializeField] public BlockDirectionInformation[] BlockDirectionInformation { get; private set; }

        /*
         * Checks angle against all blocking regions. Also gives back the information for the region that is actually doing the blocking as it has other information
         * like how much damage we actually do block
         */
        public bool IsBlocked(float angle, out BlockDirectionInformation blockDirectionInformation)
        {
            blockDirectionInformation = null;

            foreach (var directionInformation in BlockDirectionInformation)
            {
                var blocked = directionInformation.IsAngleBetween(angle);
                
                if (!blocked)
                    continue;
                
                blockDirectionInformation = directionInformation;
                return true;
            }

            return false;
        }
    }

    /*
     * Information for a single blocking region
     */
    [Serializable]
    public class BlockDirectionInformation
    {
        [Range(-180f, 180f)] public float MinAngle;
        [Range(-180f, 180f)] public float MaxAngle;
        [Range(0f, 1f)] public float DamageAbsorption;
        [Range(0f, 1f)] public float KnockBackAbsorption;
        [Range(0f, 1f)] public float PoiseDamageAbsorption;

        public bool IsAngleBetween(float angle)
        {
            if (MaxAngle > MinAngle)
            {
                return angle >= MinAngle && angle <= MaxAngle;
            }

            return (angle >= MinAngle && angle <= 180f) || (angle <= MaxAngle && angle >= -180f);
        }
    }
}