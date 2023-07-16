using System;
using Bardent.ModifierSystem;
using Bardent.Utilities;
using Bardent.Weapons.Components;
using UnityEngine;
using DamageData = Bardent.Combat.Damage.DamageData;
using Movement = Bardent.CoreSystem.Movement;

namespace Bardent.Weapons.Modifiers.BlockModifiers
{
    /*
     * The modifier used by the Block weapon component to block incoming damage by modifying the damage amount.
     */
    public class BlockDamageModifier : Modifier<DamageData>
    {
        // Event that fires off if the block was actually successful
        public event Action<GameObject> OnBlock;

        // The function that we call to determine if a block was successful. 
        private readonly BlockConditionDelegate isBlocked;

        public BlockDamageModifier(BlockConditionDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        /*
         * The meat and potatoes. Damage data is passed in when player gets damaged (before damage is applied). This modifier then
         * checks the angle of the attacker to the player and compares that to the block data angles. If block is successful, damage amount is modified
         * based on the DamageAbsorption field. If not successful, data is not modified.
         */
        public override DamageData ModifyValue(DamageData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInformation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInformation.DamageAbsorption));
                OnBlock?.Invoke(value.Source);
            }

            return value;
        }
    }
}