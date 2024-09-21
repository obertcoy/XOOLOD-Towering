using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightStoppingState : PlayerStoppingState
{
    public PlayerLightStoppingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.LightDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;

    }
    #endregion
}
