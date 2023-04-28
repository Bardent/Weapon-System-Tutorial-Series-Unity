using System;
using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    /// <summary>
    /// Base class for any projectile components to implement repeated functionality
    /// </summary>
    public class ProjectileComponent : MonoBehaviour
    {
        protected Projectile projectile;

        protected Rigidbody2D rb => projectile.Rigidbody2D;

        /// <summary>
        /// This function is called whenever the projectile is fired, indicating the start of it's journey
        /// </summary>
        protected virtual void Init()
        {
            
        }
        
        #region Plumbing

        private void Awake()
        {
            projectile = GetComponent<Projectile>();
        }

        #endregion
    }
}