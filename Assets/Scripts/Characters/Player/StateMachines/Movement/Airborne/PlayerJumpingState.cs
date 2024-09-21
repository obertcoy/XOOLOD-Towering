using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpingState : PlayerAirborneState
{

    private PlayerJumpData jumpData;
    private bool shouldKeepRotating;
    private bool canStartFalling;

    public PlayerJumpingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        jumpData = airborneData.JumpData;
    }

    #region IState methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = 0f; // Stop from moving
        stateMachine.ReusableData.RotationData = jumpData.RotationData;
        stateMachine.ReusableData.MovementDecelerationForce = jumpData.DecelerationForce;

        shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

        Jump();
    }

    public override void Exit()
    {
        base.Exit();

        SetBaseRotationData();

        canStartFalling = false;
    }

    public override void Update()
    {
        base.Update();

        if(!canStartFalling && IsMovingUp(0f))
        {
            canStartFalling = true;
        }

        if (!canStartFalling || GetPlayerYVelocity().y > 0) return;

        stateMachine.ChangeState(stateMachine.FallingState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (shouldKeepRotating) RotateTowardsTargetRotation();

        if (IsMovingUp())
        {
            DecelerateVertically();
        }

    }


    #endregion

    #region Main methods
    private void Jump()
    {
        Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;
        Vector3 jumpDirection = stateMachine.Player.transform.forward;

        if (shouldKeepRotating)
        {
            UpdateTargetRotation(GetMovementInputDirection());

            jumpDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
        }

        jumpForce.x *= jumpDirection.x;
        jumpForce.z *= jumpDirection.z;

        ResetVelocity();

        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
    }
    #endregion

    #region Input Methods

    protected override void OnMovementCanceled(InputAction.CallbackContext ctx)
    {
       
    }

    #endregion
}
