using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDrawingSwordState : PlayerSwordState
{
    public PlayerDrawingSwordState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        ChangeToCombatAnimation();

        StartTriggerAnimation(stateMachine.Player.AnimationData.CombatAnimationData.DrawingSwordParameterHash);

    }

    public override void Exit()
    {
        base.Exit();

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.CombatParameterHash);

        stateMachine.Player.LockSystem.EnableLock();
    }

    public override void OnAnimationEnterEvent()
    {
        base.OnAnimationEnterEvent();

        DrawWeapon();
    }


    #endregion
}
