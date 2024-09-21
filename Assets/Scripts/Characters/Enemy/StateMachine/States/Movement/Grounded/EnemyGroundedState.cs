using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundedState : EnemyMovementState
{


    protected Vector3 spawnPosition;

    protected Vector3[] patrolWaypoints;

    public EnemyGroundedState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        spawnPosition = stateMachine.Enemy.transform.position;

        patrolWaypoints = GeneratePatrolWaypoints();
    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.GroundedParameterHash);

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.GroundedParameterHash);
    }

    public override void Update()
    {
        base.Update();

    }

    #endregion

    #region IDamagable Methods

    public override void TakeDamage(IDamageable source, float damage)
    {
        stateMachine.ChangeState(stateMachine.TakingDamageState);
    }

    #endregion

    #region Main Methods

    private Vector3[] GeneratePatrolWaypoints()
    {

        int waypointCount = stateMachine.Enemy.Data.GroundedData.WalkData.PatrolWaypointCount;

        float patrolRange = stateMachine.Enemy.Data.GroundedData.WalkData.PatrolRange;

        Vector3[] newWaypoints = new Vector3[waypointCount];

        for (int i = 0; i < waypointCount; i++)
        {

            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(patrolRange / 2, patrolRange);

            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

            newWaypoints[i] = spawnPosition + new Vector3(x, 0f, z);

        }

        return newWaypoints;
    }

    #endregion

    #region Reusable Methods
  

    #endregion


}
