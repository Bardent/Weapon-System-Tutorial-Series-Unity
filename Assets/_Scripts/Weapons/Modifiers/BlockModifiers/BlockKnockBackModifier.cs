using Bardent.Combat.KnockBack;
using Bardent.ModifierSystem;

namespace Bardent.Weapons.Modifiers.BlockModifiers
{
    public class BlockKnockBackModifier : Modifier<KnockBackData>
    {
        private readonly BlockConditionDelegate isBlocked;

        public BlockKnockBackModifier(BlockConditionDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        public override KnockBackData ModifyValue(KnockBackData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInformation))
            {
                value.Strength *= (1 - blockDirectionInformation.KnockBackAbsorption);
            }

            return value;
        }
    }
}