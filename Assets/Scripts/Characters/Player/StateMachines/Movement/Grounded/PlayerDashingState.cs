using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashingState : PlayerGroundedState
{

    private PlayerDashData dashData;
    private float startTime;
    private int consecutiveCounter;
    private bool shouldKeepRotating;

    public PlayerDashingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        dashData = movementData.DashData;
    }

    #region IState Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = dashData.SpeedModifier;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.DashParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

        stateMachine.ReusableData.RotationData = dashData.RotationData;

        Dash();

        shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

        UpdateConsecutiveDashes();

        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.DashParameterHash);

        SetBaseRotationData();
    }

    public override void OnAnimationTransitionEvent()
    {

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.HardStoppingState);
            return;
        }

        stateMachine.ChangeState(stateMachine.RunningState);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!shouldKeepRotating) return;

        RotateTowardsTargetRotation();
    }

    #endregion

    #region Main Methods
    private void Dash()
    {

        Vector3 dashDirection = stateMachine.Player.transform.forward;
        dashDirection.y = 0f;

        UpdateTargetRotation(dashDirection, false);

        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            UpdateTargetRotation(GetMovementInputDirection());

            dashDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
        }

        stateMachine.Player.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);

    }
    private void UpdateConsecutiveDashes()
    {
        if (!IsConsecutive())
        {
            consecutiveCounter = 0;
        }

        ++consecutiveCounter;
        if(consecutiveCounter == dashData.ConsesutiveLimit)
        {
            consecutiveCounter = 0;
            stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, dashData.ConsecutiveCooldown);
        }
        
    }

    private bool IsConsecutive()
    {
        return Time.time < startTime + dashData.ConsecutiveInterval;
    }

    #endregion

    #region Reusable methods

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
    }

    #endregion

    #region Input Methods

    private void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        shouldKeepRotating = true;
    }

    protected override void OnDashStarted(InputAction.CallbackContext ctx)
    {
        
    }

    protected override void OnCombatToggleStarted(InputAction.CallbackContext ctx)
    {
    }

    #endregion


}



