using Bardent.Combat.Damage;
using UnityEngine;
using static Bardent.Utilities.CombatUtilities;

namespace Bardent.Weapons.Components
{
    public class DamageOnBlock : WeaponComponent<DamageOnBlockData, AttackDamage>
    {
        private Block block;

        private void HandleBlock(GameObject blockedGameObject)
        {
            Damage(blockedGameObject, new DamageData(currentAttackData.Amount, Core.Root));
        }

        protected override void Start()
        {
            base.Start();

            block = GetComponent<Block>();
            
            block.OnBlock += HandleBlock;
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            block.OnBlock -= HandleBlock;
        }
    }
}