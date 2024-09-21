using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunningState : EnemyMovingState
{
    public EnemyRunningState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {

        stateMachine.ReusableData.MovementSpeedModifier = stateMachine.Enemy.Data.GroundedData.RunData.SpeedModifier;

        stateMachine.ReusableData.CurrentChaseCooldown = stateMachine.Enemy.Data.CombatData.ChaseCooldown;

        base.Enter();

        IsInBasicAttackRange();

        StartAnimation(stateMachine.Enemy.AnimationData.RunningParameterHash);

        ChasePlayer();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.RunningParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if(stateMachine.ReusableData.NearestPlayer == null) 
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }

    #endregion

    #region Main Methods
  

    #endregion

}
