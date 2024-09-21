using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingState : EnemyGroundedState
{
    public EnemyMovingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.MovingParameterHash);

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.MovingParameterHash);
    }

    #endregion
}
