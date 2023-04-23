using System.Collections;
using System.Collections.Generic;
using Bardent.Weapons;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;

    private int inputIndex;
    
    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName,
        Weapon weapon,
        CombatInputs input
    ) : base(player, stateMachine, playerData, animBoolName)
    {
        this.weapon = weapon;

        inputIndex = (int)input;

        weapon.OnExit += ExitHandler;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        weapon.CurrentInput = player.InputHandler.AttackInputs[inputIndex];
    }

    public override void Enter()
    {
        base.Enter();
        
        weapon.Enter();
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}