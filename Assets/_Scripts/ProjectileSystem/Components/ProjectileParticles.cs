using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    public class ProjectileParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem impactParticles;

        public void SpawnImpactParticles(Vector3 position, Quaternion rotation)
        {
            Instantiate(impactParticles, position, rotation);
        }

        public void SpawnImpactParticles(RaycastHit2D hit)
        {
            var rotation = Quaternion.FromToRotation(transform.right, hit.normal);
            
            SpawnImpactParticles(hit.point, rotation);
        }

        public void SpawnImpactParticles(RaycastHit2D[] hits)
        {
            if(hits.Length <= 0 )
                return;
            
            SpawnImpactParticles(hits[0]);
        }
    }
}