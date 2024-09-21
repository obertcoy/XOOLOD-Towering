using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerGroundedState
{
    public PlayerDeadState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        stateMachine.Player.Rigidbody.velocity = Vector3.zero;

        stateMachine.ReusableData.CombatToggle = false;

        ChangeToCombatAnimation();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.DeadParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.DeadParameterHash);
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.Player.SpawnManager.SpawnAtMainScene(stateMachine.Player);
    }

    #endregion

    #region Reusable Methods

    protected override void AddInputActionsCallbacks()
    {

    }

    protected override void RemoveInputActionsCallbacks()
    {
        
    }

    #endregion

}
