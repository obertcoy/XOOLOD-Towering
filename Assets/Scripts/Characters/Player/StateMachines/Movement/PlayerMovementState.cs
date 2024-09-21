using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : PlayerBaseState
{

    protected PlayerGroundedData movementData;
    protected PlayerAirborneData airborneData;

    public PlayerMovementState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        movementData = stateMachine.Player.Data.GroundedData;
        airborneData = stateMachine.Player.Data.AirborneData;

        SetBaseCameraRecenteringData();

        InitializeData();
    }

    private void InitializeData()
    {
        SetBaseRotationData();
    }

    #region IState Methods

    public override void HandleInput()
    {
        ReadMovementInput();
    }

    public override void PhysicsUpdate()
    {
        Move();

    }

    #endregion

    #region Main methods

    private void ReadMovementInput()
    {
        stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f)
        {
            return;
        }

        Vector3 movementDirection = GetMovementInputDirection();

        float targetRotationYAngle = Rotate(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();
        Vector3 currPlayerXVelocity = GetPlayerXVelocity();

        stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currPlayerXVelocity, ForceMode.VelocityChange);
    }

    private float Rotate(Vector3 direction)
    {
        float directionAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation();

        return directionAngle;
    }

    #endregion

    #region Reusable Methods

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        stateMachine.Player.Input.PlayerActions.Look.started += OnMouseMovementStarted;
        stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        stateMachine.Player.Input.PlayerActions.OpenStats.performed += OnOpenStatsStarted;
    }

  
    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        stateMachine.Player.Input.PlayerActions.Look.started -= OnMouseMovementStarted;
        stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        stateMachine.Player.Input.PlayerActions.OpenStats.performed -= OnOpenStatsStarted;
    }

    protected void SetBaseCameraRecenteringData()
    {
        stateMachine.ReusableData.BackwardsCameraRecenteringData = movementData.BackwardsCameraRecenteringData;
        stateMachine.ReusableData.SidewaysCameraRecenteringData = movementData.SidewaysCameraRecenteringData;
    }

    protected void SetBaseRotationData()
    {
        stateMachine.ReusableData.RotationData = movementData.BaseRotationData;
        stateMachine.ReusableData.TimeToReachTargetRotation = stateMachine.ReusableData.RotationData.TargetRotationReachTime;
    }

    protected float GetMovementSpeed(bool shouldConsiderSlope = true)
    {
        float movementSpeed = movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier * stateMachine.Player.Stats.CheatData.SpeedModifier;

        if (shouldConsiderSlope)
        {
            movementSpeed *= stateMachine.ReusableData.MovementOnSlopesSpeedModifier;
        }

        return movementSpeed;
    }
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f, stateMachine.ReusableData.MovementInput.y);
    }


    protected Vector3 GetPlayerXVelocity()
    {
        Vector3 playerVelocity = stateMachine.Player.Rigidbody.velocity;
        playerVelocity.y = 0f;
        return playerVelocity;
    }

    protected Vector3 GetPlayerYVelocity()
    {
        Vector3 playerVelocity = stateMachine.Player.Rigidbody.velocity;
        playerVelocity.x = 0f;
        playerVelocity.z = 0f;
        return playerVelocity;


    }

    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;
    }

    protected void ResetVerticalVelocity()
    {
        Vector3 playerHorizontalVelocity = GetPlayerXVelocity();

        stateMachine.Player.Rigidbody.velocity = playerHorizontalVelocity;
    }


    protected void DecelerateHorizontally()
    {
        Vector3 playerXVelocity = GetPlayerXVelocity();

        stateMachine.Player.Rigidbody.AddForce(-playerXVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
    }
    protected void DecelerateVertically()
    {
        Vector3 playerYVelocity = GetPlayerYVelocity();

        stateMachine.Player.Rigidbody.AddForce(-playerYVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
    }

    protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
    {
        Vector3 playerXVelocity = GetPlayerXVelocity();
        Vector2 playerXMovement = new Vector2(playerXVelocity.x, playerXVelocity.z);
        return playerXMovement.magnitude > minimumMagnitude;
    }


    protected void EnableCameraRecentering(float waitTime = -1f, float recenteringTime = -1f)
    {

        float movementSpeed = GetMovementSpeed();

        if (movementSpeed == 0f) movementSpeed = movementData.BaseSpeed;

        stateMachine.Player.CameraUtility.EnableRecentering(waitTime, recenteringTime, movementData.BaseSpeed, movementSpeed);

    }

    protected void DisableCameraRecentering()
    {
        stateMachine.Player.CameraUtility.DisableRecentering();
    }

    protected virtual bool IsMovingUp(float minimumVelocity = 0.1f)
    {
        return GetPlayerYVelocity().y > minimumVelocity;
    }

    protected virtual bool IsMovingDown(float minimumVelocity = 0.1f)
    {
        return GetPlayerYVelocity().y < -minimumVelocity;
    }

    protected void UpdateCameraRecenteringState(Vector2 movementInput)
    {
        if (movementInput == Vector2.zero) return;

        if (movementInput == Vector2.up)
        {
            DisableCameraRecentering();

            return;
        }

        float cameraVerticalAngle = stateMachine.Player.MainCameraTransform.eulerAngles.x;

        if (cameraVerticalAngle >= 270f) cameraVerticalAngle -= 360f;

        cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);

        if (movementInput == Vector2.down)
        {
            SetCameraRecenteringState(cameraVerticalAngle, stateMachine.ReusableData.BackwardsCameraRecenteringData);

            return;
        }

        SetCameraRecenteringState(cameraVerticalAngle, stateMachine.ReusableData.SidewaysCameraRecenteringData);
    }

    protected void SetCameraRecenteringState(float cameraVerticalAngle, List<PlayerCameraRecenteringData> cameraRecenteringData)
    {
        foreach (PlayerCameraRecenteringData recenteringData in cameraRecenteringData)
        {
            if (!recenteringData.IsWithinRange(cameraVerticalAngle)) continue;

            EnableCameraRecentering(recenteringData.WaitTime, recenteringData.RecenteringTime);

            return;
        }

        DisableCameraRecentering();
    }

    #endregion

    #region Input Methods

    protected virtual void OnWalkToggleStarted(InputAction.CallbackContext ctx)
    {
        stateMachine.ReusableData.WalkToggle = !stateMachine.ReusableData.WalkToggle;
    }

    private void OnMouseMovementStarted(InputAction.CallbackContext ctx)
    {
        UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
    }

    private void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        UpdateCameraRecenteringState(ctx.ReadValue<Vector2>());
    }
    protected virtual void OnMovementCanceled(InputAction.CallbackContext ctx)
    {
        DisableCameraRecentering();
    }
    private void OnOpenStatsStarted(InputAction.CallbackContext obj)
    {
        stateMachine.Player.Stats.OpenStatsMenuUI();
    }

    #endregion

    #region IDamageable Methods

    public override void TakeDamage(IDamageable source, float damage)
    {
        base.TakeDamage(source, damage);

        if(stateMachine.Player.Stats.RuntimeData.Health <= 0) stateMachine.ChangeState(stateMachine.DeadState);
    }

    #endregion
}
