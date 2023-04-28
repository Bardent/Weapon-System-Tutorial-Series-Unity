using System;
using UnityEngine;

namespace Bardent.ProjectileSystem.Components
{
    public class ProjectileComponent : MonoBehaviour
    {
        protected Projectile projectile;

        #region Plumbing

        private void Awake()
        {
            projectile = GetComponent<Projectile>();
        }

        #endregion
    }
}