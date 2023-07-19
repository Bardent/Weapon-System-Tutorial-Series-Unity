using Bardent.Combat.PoiseDamage;
using Bardent.ModifierSystem;

namespace Bardent.Weapons.Modifiers.BlockModifiers
{
    public class BlockPoiseDamageModifier : Modifier<PoiseDamageData>
    {
        private readonly BlockConditionDelegate isBlocked;

        public BlockPoiseDamageModifier(BlockConditionDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        public override PoiseDamageData ModifyValue(PoiseDamageData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInformation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInformation.PoiseDamageAbsorption));
            }

            return value;
        }
    }
}