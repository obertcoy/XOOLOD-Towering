using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdlingState : EnemyGroundedState
{
    private bool waiting;

    public EnemyIdlingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        stateMachine.Enemy.NavMeshAgent.ResetPath();

        waiting = false;
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.ReusableData.NearestPlayer && !waiting)
        {
            waiting = true;

            stateMachine.Enemy.StartCoroutine(WaitAndChangeState(stateMachine.WalkingState ,stateMachine.Enemy.Data.GroundedData.WalkData.PatrolCooldown));
        }
    }

    #endregion

    #region Main Methods

   

    #endregion
}
