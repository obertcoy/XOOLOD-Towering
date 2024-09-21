using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : EnemyBaseState
{

    protected int playerLayerMask = 1 << LayerMask.NameToLayer("Player");

    protected EnemyGroundedData groundedData;
    public EnemyMovementState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        groundedData = enemyStateMachine.Enemy.Data.GroundedData;
    }

    #region IState Methods

    public override void Update()
    {

        UpdateCooldown();

        CheckAttack();

        CheckGroundedAggroRange();

        if (stateMachine.Enemy.Data.AirborneData.EnableFlying)
        {
            CheckFlyingAggroRange();
        }
    }

    public override void PhysicsUpdate()
    {
        Move();

        LookAtPlayer();
    }
    #endregion

    #region Main Methods
    private void Move()
    {
        float movementSpeed = groundedData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;

        stateMachine.Enemy.NavMeshAgent.speed = movementSpeed;

    }

    private void UpdateCooldown()
    {
        stateMachine.ReusableData.CurrentAttackIntervalCooldown -= Time.deltaTime;
        stateMachine.ReusableData.CurrentChaseCooldown -= Time.deltaTime;
        stateMachine.ReusableData.CurrentPatrolCooldown -= Time.deltaTime;

        stateMachine.ReusableData.CurrentAttack1Cooldown -= Time.deltaTime;
        stateMachine.ReusableData.CurrentAttack2Cooldown -= Time.deltaTime;
        stateMachine.ReusableData.CurrentAttack3Cooldown -= Time.deltaTime;
        stateMachine.ReusableData.CurrentAttack4Cooldown -= Time.deltaTime;

        stateMachine.ReusableData.CurrentTakeDamageAnimationCooldown -= Time.deltaTime;
    }

    private void CheckAttack()
    {
        if (stateMachine.ReusableData.NearestPlayer != null && stateMachine.ReusableData.CurrentAttackIntervalCooldown <= 0)
        {
            int idx = GetRandomAttack();


            if(stateMachine.ReusableData.GetCurrentAttackCooldown(idx) <= 0)
            {

                if (Vector3.Distance(stateMachine.Enemy.transform.position, stateMachine.ReusableData.NearestPlayer.transform.position) <= stateMachine.Enemy.Data.CombatData.AttackData[idx].AttackRange)
                {

                    stateMachine.ChangeState(GetAttackState(idx)!);

                    return;
                }

            }
        }
    }

    #endregion

    #region Reusable Methods

    protected IEnumerator WaitAndChangeState(IState state, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        stateMachine.ChangeState(state);
    }

    protected void ChasePlayer()
    {
        if (stateMachine.ReusableData.NearestPlayer != null)
        {
            stateMachine.Enemy.NavMeshAgent.SetDestination(stateMachine.ReusableData.NearestPlayer.transform.position);

        }
    }

    protected void CheckHealthBelowZero()
    {
        if (stateMachine.Enemy.Stats.RuntimeData.Health <= 0) stateMachine.ChangeState(stateMachine.TakingDamageState);
    }
    protected void LookAtPlayer()
    {

        if (!stateMachine.ReusableData.NearestPlayer) return;

        Vector3 direction = (stateMachine.ReusableData.NearestPlayer.transform.position - stateMachine.Enemy.transform.position).normalized;

        Quaternion rotationGoal = Quaternion.LookRotation(direction);

        stateMachine.Enemy.transform.rotation = Quaternion.Slerp(stateMachine.Enemy.transform.rotation, rotationGoal, stateMachine.Enemy.Data.GroundedData.RotationSpeed);


    }

    protected void CheckGroundedAggroRange()
    {
        Collider[] aggroColliders = Physics.OverlapSphere(stateMachine.Enemy.transform.position, stateMachine.Enemy.Data.CombatData.AggroRange, playerLayerMask);

        if (aggroColliders.Length > 0)
        {

            FindNearestPlayer(aggroColliders);

            if (stateMachine.ReusableData.CurrentChaseCooldown <= 0)
            {

                stateMachine.ChangeState(stateMachine.RunningState);

                return;
            }

        }
        else
        {
            stateMachine.ReusableData.NearestPlayer = null;
        }

    }

    protected void CheckFlyingAggroRange()
    {
        Collider[] aggroColliders = Physics.OverlapSphere(stateMachine.Enemy.transform.position, stateMachine.Enemy.Data.CombatData.FlyingAggroRange, playerLayerMask);

        if (aggroColliders.Length > 0)
        {

            FindNearestPlayer(aggroColliders, stateMachine.Enemy.Data.CombatData.AggroRange, stateMachine.Enemy.Data.CombatData.FlyingAggroRange);

            if (stateMachine.ReusableData.CurrentChaseCooldown <= 0)
            {
                stateMachine.ChangeState(stateMachine.FlyingState);

                return;
            }

        }
        else
        {
            stateMachine.ReusableData.NearestPlayer = null;
        }

    }


    protected void FindNearestPlayer(Collider[] colliders, float minDistance = -1, float maxDistance = -1)
    {
        float nearestDistance = float.MaxValue;
        stateMachine.ReusableData.NearestPlayer = null;

      
            foreach (Collider collider in colliders)
            {
                Transform playerTransform = collider.transform;

                float playerDistance = Vector3.Distance(stateMachine.Enemy.transform.position, playerTransform.position);

                if (playerDistance < nearestDistance)
                {
                    nearestDistance = playerDistance;
                    stateMachine.ReusableData.NearestPlayer = playerTransform;
                }
            }

    }

    protected int GetRandomAttack()
    {
        int attackCount = stateMachine.Enemy.Data.CombatData.AttackData.Count;

        int randAttack = Random.Range(0, attackCount);

        return randAttack;
    }

    protected IState GetAttackState(int idx)
    {
        if (idx == 0) return stateMachine.Attacking1State;
        if (idx == 1) return stateMachine.Attacking2State;
        if (idx == 2) return stateMachine.Attacking3State;
        if (idx == 3) return stateMachine.Attacking4State;

        return null;
    }

    protected bool IsInBasicAttackRange()
    {
        if (Vector3.Distance(stateMachine.Enemy.transform.position, stateMachine.ReusableData.NearestPlayer.transform.position)
         <= stateMachine.Enemy.Data.CombatData.AttackData[0].AttackRange)
        {

            stateMachine.ChangeState(stateMachine.IdlingState);

            return true;
        }

        return false;
    }

    #endregion

}
