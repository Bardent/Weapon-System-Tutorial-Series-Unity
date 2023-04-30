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

        protected virtual void Awake()
        {
            projectile = GetComponent<Projectile>();

            projectile.OnInit += Init;
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        {
            
        }

        protected virtual void OnDestroy()
        {
            projectile.OnInit -= Init;
        }

        #endregion
    }
}