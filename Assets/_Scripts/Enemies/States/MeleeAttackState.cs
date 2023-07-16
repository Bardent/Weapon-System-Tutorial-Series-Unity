using System.Collections;
using System.Collections.Generic;
using Bardent.Combat.Damage;
using Bardent.Combat.KnockBack;
using Bardent.CoreSystem;
using UnityEngine;

public class MeleeAttackState : AttackState {
	private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

	private Movement movement;
	private CollisionSenses collisionSenses;

	protected D_MeleeAttack stateData;

	public MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(etity, stateMachine, animBoolName, attackPosition) {
		this.stateData = stateData;
	}
	
	public override void TriggerAttack() {
		base.TriggerAttack();

		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

		foreach (Collider2D collider in detectedObjects) {
			IDamageable damageable = collider.GetComponent<IDamageable>();

			if (damageable != null) {
				damageable.Damage(new DamageData(stateData.attackDamage, core.Root));
			}

			IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();

			if (knockBackable != null) {
				knockBackable.KnockBack(new KnockBackData(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection, core.Root));
			}
		}
	}
}
