using UnityEngine;

namespace Bardent.Combat.Parry
{
    public class ParryData
    {
        public GameObject Source { get; private set; }

        public ParryData(GameObject source)
        {
            Source = source;
        }
    }
}