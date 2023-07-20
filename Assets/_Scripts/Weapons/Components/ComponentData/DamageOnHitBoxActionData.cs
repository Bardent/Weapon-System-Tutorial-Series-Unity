namespace Bardent.Weapons.Components
{
    public class DamageOnHitBoxActionData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(DamageOnHitBoxAction);
        }
    }
}