using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyGroundedState
{
    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        stateMachine.Enemy.NavMeshAgent.enabled = false;
        stateMachine.Enemy.Rigidbody.freezeRotation = true;

        StartAnimation(stateMachine.Enemy.AnimationData.DeadParameterHash);

        stateMachine.Enemy.tag = "Dead";

        stateMachine.Enemy.StartCoroutine(DestroyEnemy());
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.DeadParameterHash);
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate()
    {

    }

    #endregion

    #region Main Methods

    private IEnumerator DestroyEnemy()
    {

        DeadEffect.Create(stateMachine.Enemy.transform.position, stateMachine.Enemy.gameObject);

        stateMachine.ReusableData.LatestSource.GetDropLoot(
            stateMachine.Enemy.Stats.DefaultData.ExpDrop,
            stateMachine.Enemy.Stats.DefaultData.GoldDrop);

        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus)) WinCanvas.Create(Player.Instance);

        if (stateMachine.Enemy.Spawner != null)
        {
            stateMachine.Enemy.Spawner.HandleEnemyDied(stateMachine.Enemy.Data.Name);
        }

        yield return new WaitForSeconds(1.5f);

        stateMachine.Enemy.DestroyObject(stateMachine.Enemy.gameObject);
    }

    #endregion

}
