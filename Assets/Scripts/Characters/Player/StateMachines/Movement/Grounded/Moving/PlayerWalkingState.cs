using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMovingState
{

    private PlayerWalkData walkData;

    public PlayerWalkingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        walkData = movementData.WalkData;
    }

    #region IState methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;

        stateMachine.ReusableData.BackwardsCameraRecenteringData = walkData.BackwardsCameraRecenteringData;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.WalkParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;

    }
    
    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.WalkParameterHash);

        SetBaseCameraRecenteringData();
    }

    #endregion

    #region Input methods

    protected override void OnMovementCanceled(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.LightStoppingState);
        base.OnMovementCanceled(ctx);
    }

    protected override void OnWalkToggleStarted(InputAction.CallbackContext ctx)
    {
        base.OnWalkToggleStarted(ctx);
        stateMachine.ChangeState(stateMachine.RunningState);
    }

    #endregion
}
