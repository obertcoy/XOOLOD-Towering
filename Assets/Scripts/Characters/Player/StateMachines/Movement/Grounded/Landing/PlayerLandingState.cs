using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.LandingParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.LandingParameterHash);
    }

    #endregion


    #region Input Methods

    protected override void OnCombatToggleStarted(InputAction.CallbackContext ctx)
    {

    }

    protected override void OnBasicAttackStarted(InputAction.CallbackContext ctx)
    {
       
    }

    protected override void OnAbility1Started(InputAction.CallbackContext ctx)
    {
      
    }
    protected override void OnAbility2Started(InputAction.CallbackContext ctx)
    {

    }
    protected override void OnAbility3Started(InputAction.CallbackContext ctx)
    {

    }
    protected override void OnAbility4Started(InputAction.CallbackContext ctx)
    {

    }


    #endregion
}
