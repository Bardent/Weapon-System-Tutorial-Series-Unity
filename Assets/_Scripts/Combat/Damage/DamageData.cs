using UnityEngine;

namespace Bardent.Combat.Damage
{
    /*
     * The DamageData class holds information we want to pass through the IDamageable interface.
     */
    public class DamageData
    {
        public float Amount { get; private set;}
        public GameObject Source { get; private set;}

        public DamageData(float amount, GameObject source)
        {
            Amount = amount;
            Source = source;
        }

        public void SetAmount(float amount)
        {
            Amount = amount;
        }
    }
}