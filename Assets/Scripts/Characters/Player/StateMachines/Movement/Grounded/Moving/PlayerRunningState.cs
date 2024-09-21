using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

        if (stateMachine.ReusableData.CombatToggle)
        {
            stateMachine.ReusableData.MovementSpeedModifier = stateMachine.ReusableData.MovementSpeedModifier * movementData.RunData.CombatSpeedModifier;
        }

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.RunParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.RunParameterHash);
    }

    #endregion

    #region Input methods
    protected override void OnMovementCanceled(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.MediumStoppingState);

        base.OnMovementCanceled(ctx);
    }

    protected override void OnWalkToggleStarted(InputAction.CallbackContext ctx)
    {
        base.OnWalkToggleStarted(ctx);
        stateMachine.ChangeState(stateMachine.WalkingState);
    }


    #endregion
}
