namespace Bardent.Weapons.Components
{
    public class DrawToProjectileData : ComponentData
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(DrawToProjectile);
        }
    }
}