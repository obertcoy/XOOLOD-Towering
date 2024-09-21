using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingState : EnemyAirboneState
{
    public EnemyFlyingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {

        if (IsInBasicAttackRange())
        {
            stateMachine.ChangeState(stateMachine.LandingState);
        }

        if (!stateMachine.ReusableData.isAirborne)
        {
            stateMachine.ReusableData.NextDesiredState = stateMachine.FlyingState;

            stateMachine.ChangeState(stateMachine.TakingOffState);
        }
        else
        {
            stateMachine.ReusableData.MovementSpeedModifier = stateMachine.Enemy.Data.AirborneData.FlyingData.SpeedModifier;

            stateMachine.ReusableData.CurrentChaseCooldown = stateMachine.Enemy.Data.CombatData.ChaseCooldown;

            base.Enter();

            StartAnimation(stateMachine.Enemy.AnimationData.FlyingParameterHash);

            ChasePlayer();
        }
       
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.FlyingParameterHash);
    }

    public override void Update()
    {
        Debug.Log("Airborne: " + stateMachine.ReusableData.isAirborne);

        if (!stateMachine.ReusableData.isAirborne) return;

        CheckFlyingAggroRange();

        if (stateMachine.Enemy.NavMeshAgent.remainingDistance <= stateMachine.Enemy.NavMeshAgent.stoppingDistance)
        {
            stateMachine.ChangeState(stateMachine.LandingState);
        }

    }

    #endregion
}
