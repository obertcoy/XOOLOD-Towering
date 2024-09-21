using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdlingState : PlayerGroundedState
{

    private PlayerIdleData idleData;
    public PlayerIdlingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        stateMachine.Player.Data.CombatData.SwordData.InitializeGameObject();
        idleData = movementData.IdleData;
    }

    public override void Enter()
    {

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.BackwardsCameraRecenteringData = idleData.BackwardsCameraRecenteringData;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.IdleParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

        ResetVelocity();

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero) return;

        OnMove();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!IsMovingHorizontally()) return;

        ResetVelocity();
    }

    #region Main Methods

  
    #endregion

}