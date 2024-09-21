using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLandingState : EnemyGroundedState
{
    public EnemyLandingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.isAirborne = false;

        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.LandingParameterHash);


    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.LandingParameterHash);
    }

    public override void Update()
    {

    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    #endregion
}
