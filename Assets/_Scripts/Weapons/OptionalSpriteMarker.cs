using UnityEngine;

namespace Bardent.Weapons
{
    /*
     * This is an empty MonoBehaviour used to help identify a specific GameObject. This GameObject should be a child of the Base weapon GameObject
     * and is animated by the base weapon animations.
     */
    public class OptionalSpriteMarker : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => gameObject.GetComponent<SpriteRenderer>();
    }
}