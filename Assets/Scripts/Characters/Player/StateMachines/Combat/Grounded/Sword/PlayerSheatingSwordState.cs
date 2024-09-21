using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSheatingSwordState : PlayerSwordState
{
    public PlayerSheatingSwordState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartTriggerAnimation(stateMachine.Player.AnimationData.CombatAnimationData.SheatingSwordParameterHash);

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.CombatParameterHash);

        ChangeToCombatAnimation();

        stateMachine.Player.LockSystem.DisableLock();
    }

    public override void OnAnimationExitEvent()
    {
        SheathSword();

        base.OnAnimationExitEvent();

    }

}
