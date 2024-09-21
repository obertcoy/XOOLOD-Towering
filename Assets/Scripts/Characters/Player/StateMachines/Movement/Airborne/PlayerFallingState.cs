using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{

    private PlayerFallData fallData;
    private Vector3 playerPositionOnEnter;

    public PlayerFallingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        fallData = airborneData.FallData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.FallParameterHash);

        playerPositionOnEnter = stateMachine.Player.transform.position;

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        ResetVerticalVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.FallParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        LimitVerticalVelocity();
    }

    #endregion

    #region Reusable Methods

    protected override void OnContactWithGround(Collider collider)
    {

        float fallDistance = playerPositionOnEnter.y - stateMachine.Player.transform.position.y;

        if(fallDistance < fallData.HardFallTreshold)
        {
            stateMachine.ChangeState(stateMachine.LightLandingState);

            return;
        }

        if(stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.HardLandingState);

            return;
        }

        stateMachine.ChangeState(stateMachine.RollingState);
    }

    #endregion


    #region Main Methods
    private void LimitVerticalVelocity()
    {
        Vector3 playerVerticalVelocity = GetPlayerYVelocity();

        if(playerVerticalVelocity.y >= -fallData.FallSpeedLimit)
        {
            return;
        }

        Vector3 limittedVelocity = new Vector3(0f, -fallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

        stateMachine.Player.Rigidbody.AddForce(limittedVelocity, ForceMode.VelocityChange);

    }
    #endregion
}
