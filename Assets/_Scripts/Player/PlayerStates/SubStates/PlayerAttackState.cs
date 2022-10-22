using System.Collections;
using System.Collections.Generic;
using Bardent.Weapons;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;

    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName,
        Weapon weapon
    ) : base(player, stateMachine, playerData, animBoolName)
    {
        this.weapon = weapon;

        weapon.OnExit += HandleExit;
    }

    public override void Enter()
    {
        base.Enter();
        
        weapon.Enter();
    }

    private void HandleExit()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}