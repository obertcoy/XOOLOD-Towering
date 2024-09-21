using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakingDamageState : EnemyGroundedState
{

    private bool isDead;

    public EnemyTakingDamageState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        if (stateMachine.Enemy.Stats.RuntimeData.Health <= 0 && !isDead)
        {

            isDead = true;

            stateMachine.ChangeState(stateMachine.DeadState);

            return;
        }

        if (stateMachine.ReusableData.CurrentTakeDamageAnimationCooldown <= 0)
        {
            StartAnimation(stateMachine.Enemy.AnimationData.TakingDamageParameterHash);

            stateMachine.ReusableData.CurrentTakeDamageAnimationCooldown = stateMachine.Enemy.Data.CombatData.TakeDamageAnimationCooldown;
        }

        // Debug.Log("Current health: " + stateMachine.Enemy.Stats.RuntimeData.Health);

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.TakingDamageParameterHash);
    }

    #endregion
}
