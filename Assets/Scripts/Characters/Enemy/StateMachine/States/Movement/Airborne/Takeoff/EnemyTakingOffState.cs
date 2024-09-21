using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakingOffState : EnemyAirboneState
{
    public EnemyTakingOffState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.TakingOffParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.TakingOffParameterHash);
    }

    public override void Update()
    {
        
    }

    public override void OnAnimationTransitionEvent()
    {
        Debug.Log("Next desired state: " + stateMachine.ReusableData.NextDesiredState);

        if (stateMachine.ReusableData.NextDesiredState != null)
        {

            stateMachine.ChangeState(stateMachine.ReusableData.NextDesiredState);

            stateMachine.ReusableData.NextDesiredState = null;
        }

    }

    #endregion

}
