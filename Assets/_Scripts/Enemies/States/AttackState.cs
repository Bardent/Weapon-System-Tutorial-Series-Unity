using System.Collections;
using System.Collections.Generic;
using Bardent.CoreSystem;
using UnityEngine;

public class AttackState : State {

	private Movement movement;
	private ParryReceiver parryReceiver;

	protected Transform attackPosition;

	protected bool isAnimationFinished;
	protected bool isPlayerInMinAgroRange;

	public AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(etity, stateMachine, animBoolName) {
		this.attackPosition = attackPosition;

		movement = core.GetCoreComponent<Movement>();
		parryReceiver = core.GetCoreComponent<ParryReceiver>();
	}

	public override void DoChecks() {
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}

	public override void Enter() {
		base.Enter();

		entity.atsm.attackState = this;
		isAnimationFinished = false;
		movement?.SetVelocityX(0f);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		movement?.SetVelocityX(0f);
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	public virtual void TriggerAttack() {

	}

	public virtual void FinishAttack() {
		isAnimationFinished = true;
	}

	public void SetParryWindowActive(bool value) => parryReceiver.SetParryColliderActive(value);
}
