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
        
        // The current attacks block information
        private AttackBlock attackBlock;
        
        // Used to get the facing direction
        private readonly Movement movement;
        
        // Transform of the player to determine that angle of an incoming attack
        private readonly Transform receiverTransform;

        public BlockDamageModifier(Movement movement, Transform receiverTransform)
        {
            this.movement = movement;
            this.receiverTransform = receiverTransform;
        }

        // Block info gets updated to new info when attack starts
        public void SetAttackBlock(AttackBlock attackBlock) => this.attackBlock = attackBlock;

        /*
         * The meat and potatoes. Damage data is passed in when player gets damaged (before damage is applied). This modifier then
         * checks the angle of the attacker to the player and compares that to the block data angles. If block is successful, damage amount is modified
         * based on the DamageAbsorption field. If not successful, data is not modified.
         */
        public override DamageData ModifyValue(DamageData value)
        {
            var angleOfAttacker = AngleUtilities.AngleFromFacingDirection(
                receiverTransform,
                value.Source.transform,
                movement.FacingDirection
            );

            if (attackBlock.IsBlocked(angleOfAttacker, out var blockDirectionInformation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInformation.DamageAbsorption));
                OnBlock?.Invoke(value.Source);
            }

            return value;
        }
    }
}