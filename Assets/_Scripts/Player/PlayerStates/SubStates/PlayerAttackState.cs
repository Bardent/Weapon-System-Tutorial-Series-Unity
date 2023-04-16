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
        CombatInputs inputIndex
    ) : base(player, stateMachine, playerData, animBoolName)
    {
        this.weapon = weapon;

        this.inputIndex = (int)this.inputIndex;
        
        weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();
        
        weapon.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        weapon.CurrentInput = player.InputHandler.AttackInputs[inputIndex];
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}