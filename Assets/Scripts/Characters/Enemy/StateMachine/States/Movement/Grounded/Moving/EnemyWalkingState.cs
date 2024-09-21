using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkingState : EnemyMovingState
{

    private NavMeshAgent navMeshAgent;

    public EnemyWalkingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        navMeshAgent = stateMachine.Enemy.NavMeshAgent;
    }

    #region IState Methods

    public override void Enter()
    {
       
        stateMachine.ReusableData.MovementSpeedModifier = stateMachine.Enemy.Data.GroundedData.WalkData.SpeedModifier;

        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.WalkingParameterHash);

        StartPatroling();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.WalkingParameterHash);
    }

    public override void Update()
    {
        base.Update();

        CheckPatroling();
    }

    #endregion

    #region Main Methods
    private void StartPatroling()
    {
        stateMachine.ReusableData.CurrentWaypointIndex = (stateMachine.ReusableData.CurrentWaypointIndex + 1) % patrolWaypoints.Length;

        navMeshAgent.SetDestination(patrolWaypoints[stateMachine.ReusableData.CurrentWaypointIndex]);
    }

    private void CheckPatroling()
    {
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }

    #endregion

}
