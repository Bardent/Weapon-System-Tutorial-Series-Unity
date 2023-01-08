using UnityEngine;

namespace Bardent.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticles;
        
        private Stats stats;
        private ParticleManager particleManager;

        public void Damage(float amount)
        {
            Debug.Log(core.transform.parent.name + " Damaged!");
            stats?.DecreaseHealth(amount);
            particleManager?.StartParticlesWithRandomRotation(damageParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = new CoreComp<Stats>(core);
            particleManager = new CoreComp<ParticleManager>(core);
        }
    }
}