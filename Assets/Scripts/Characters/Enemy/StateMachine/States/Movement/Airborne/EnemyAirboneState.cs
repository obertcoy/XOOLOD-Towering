using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirboneState : EnemyMovementState
{
    public EnemyAirboneState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.isAirborne = true;

        StartAnimation(stateMachine.Enemy.AnimationData.AirborneParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.AirborneParameterHash);

    }

    public override void Update()
    {
        
    }

    #endregion

    #region IDamagable Methods

    public override void TakeDamage(IDamageable source, float damage)
    {
        CheckHealthBelowZero();
    }

    #endregion

}
