using System.Collections;
using System.Collections.Generic;
using Bardent.Weapons;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;

    private int inputIndex;

    private bool canInterrupt;

    private bool checkFlip;

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

        weapon.OnUseInput += HandleUseInput;

        weapon.EventHandler.OnEnableInterrupt += HandleEnableInterrupt;
        weapon.EventHandler.OnFinish += HandleFinish;
        weapon.EventHandler.OnFlipSetActive += HandleFlipSetActive;
    }

    private void HandleFlipSetActive(bool value)
    {
        checkFlip = value;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        var playerInputHandler = player.InputHandler;

        var xInput = playerInputHandler.NormInputX;
        var attackInputs = playerInputHandler.AttackInputs;

        weapon.CurrentInput = attackInputs[inputIndex];

        if (checkFlip)
        {
            Movement.CheckIfShouldFlip(xInput);
        }

        if (!canInterrupt)
            return;

        if (xInput != 0 || attackInputs[0] || attackInputs[1])
        {
            isAbilityDone = true;
        }
    }

    public override void Enter()
    {
        base.Enter();

        checkFlip = true;
        canInterrupt = false;

        weapon.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        weapon.Exit();
    }

    private void HandleEnableInterrupt() => canInterrupt = true;

    private void HandleUseInput() => player.InputHandler.UseAttackInput(inputIndex);

    private void HandleFinish()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}