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
        [field: SerializeField] public DirectionalInformation[] BlockDirectionInformation { get; private set; }

        [field: SerializeField] public GameObject Particles { get; private set; }

        [field: SerializeField] public Vector2 ParticlesOffset { get; private set; }

        /*
         * Checks angle against all blocking regions. Also gives back the information for the region that is actually doing the blocking as it has other information
         * like how much damage we actually do block
         */
        public bool IsBlocked(float angle, out DirectionalInformation directionalInformation)
        {
            directionalInformation = null;

            foreach (var directionInformation in BlockDirectionInformation)
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