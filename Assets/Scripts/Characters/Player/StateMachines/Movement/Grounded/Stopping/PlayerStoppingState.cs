using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStoppingState : PlayerGroundedState
{
    public PlayerStoppingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        SetBaseCameraRecenteringData();

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.StoppingParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.StoppingParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        RotateTowardsTargetRotation();

        if (!IsMovingHorizontally()) return;

        DecelerateHorizontally();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    #endregion

    #region Reusable methods

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
    }

  

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;

    }

    #endregion

    #region Input methods
    private void OnMovementStarted(InputAction.CallbackContext ctx)
    {
        OnMove();
    }


    #endregion
}
