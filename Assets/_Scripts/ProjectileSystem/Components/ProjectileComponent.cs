using System;
using Bardent.ProjectileSystem.DataPackages;
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

      
        // This function is called whenever the projectile is fired, indicating the start of it's journey
        protected virtual void Init()
        {
            
        }

        protected virtual void Reset()
        {
            
        }

        /* Handles receiving specific data from the weapon. Implemented in any component that needs to use it. Automatically subscribed for all projectile
        components by this base class (see Awake and OnDestroy) */
        protected virtual void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            
        }
        
        #region Plumbing

        protected virtual void Awake()
        {
            projectile = GetComponent<Projectile>();

            projectile.OnInit += Init;
            projectile.OnReset += Reset;
            projectile.OnReceiveDataPackage += HandleReceiveDataPackage;
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
            projectile.OnReset -= Reset;
            projectile.OnReceiveDataPackage -= HandleReceiveDataPackage;
        }

        #endregion
    }
}