namespace Bardent.Weapons.Components
{
    public class TargeterToProjectileData : ComponentData
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(TargeterToProjectile);
        }
    }
}