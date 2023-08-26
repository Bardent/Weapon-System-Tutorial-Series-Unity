namespace Bardent.Weapons.Components
{
    public class DamageOnParryData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(DamageOnParry);
        }
    }
}