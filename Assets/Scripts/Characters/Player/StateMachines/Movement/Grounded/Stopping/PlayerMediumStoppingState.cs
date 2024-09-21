using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediumStoppingState : PlayerStoppingState
{
    public PlayerMediumStoppingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.MediumStopParameterHash);

        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.MediumStopParameterHash);
    }
    #endregion
}
