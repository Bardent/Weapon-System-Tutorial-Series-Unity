namespace Bardent.Weapons.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        public DamageData()
        {
            dependency = typeof(Damage);
        }
    }
}