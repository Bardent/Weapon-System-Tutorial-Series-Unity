using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bardent.CoreSystem
{
    public class ParticleManager : CoreComponent
    {
        private Transform particleContainer;

        private Movement movement;

        protected override void Awake()
        {
            base.Awake();

            particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
        }

        private void Start()
        {
            movement = core.GetCoreComponent<Movement>();
        }

        public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
        {
            return Instantiate(particlePrefab, position, rotation, particleContainer);
        }

        public GameObject StartParticles(GameObject particlePrefab)
        {
            return StartParticles(particlePrefab, transform.position, Quaternion.identity);
        }

        public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            return StartParticles(particlePrefab, transform.position, randomRotation);
        }

        // Spawns particles relative to transform based on offset (input parameter) and FacingDirection
        public GameObject StartParticlesRelative(GameObject particlePrefab, Vector2 offset, Quaternion rotation)
        {
            var pos = FindPointRelativeToPositionAndFacingDirection(offset);

            return StartParticles(particlePrefab, pos, rotation);
        }
        
        private Vector2 FindPointRelativeToPositionAndFacingDirection(Vector2 offset)
        {
            offset.x *= movement.FacingDirection;

            return transform.position + (Vector3)offset;
        }
    }
}
