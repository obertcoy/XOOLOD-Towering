using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardStoppingState : PlayerStoppingState
{
    public PlayerHardStoppingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
        base.Enter();


        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.HardStopParameterHash);

        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.HardStopParameterHash);
    }

    #endregion

    #region Reusable methods

    protected override void OnMove()
    {
        if (stateMachine.ReusableData.WalkToggle) return;
        stateMachine.ChangeState(stateMachine.RunningState);
    }

    #endregion
}
