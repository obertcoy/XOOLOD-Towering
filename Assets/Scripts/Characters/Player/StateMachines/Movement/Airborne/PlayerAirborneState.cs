using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.AirborneParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.AirborneParameterHash);
    }

    #endregion

    #region Reusable methods

    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.LightLandingState);
    }

    #endregion
}
