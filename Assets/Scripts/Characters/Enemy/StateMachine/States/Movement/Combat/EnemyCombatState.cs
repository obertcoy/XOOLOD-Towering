using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : EnemyMovementState
{
    public EnemyCombatState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.CurrentAttackIntervalCooldown = stateMachine.Enemy.Data.CombatData.AttackIntervalCooldown;

        Debug.Log("Nearest: " + stateMachine.ReusableData.NearestPlayer);

        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.CombatParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.CombatParameterHash);

    }

    public override void Update()
    {

    }

    public override void TakeDamage(IDamageable source, float damage)
    {
        CheckHealthBelowZero();
    }

    #endregion

    #region Reusable Methods

    protected virtual GameObject ActivateAbility(EnemyActiveAbilitySO activeAbility)
    {

        Vector3 position = stateMachine.Enemy.transform.position;
        Vector3 direction = stateMachine.Enemy.transform.forward;
        Quaternion rotation = stateMachine.Enemy.transform.rotation;

        Vector3 spawnPosition = position + (direction * activeAbility.SpawnDistance);

        return stateMachine.Enemy.InstantiateGameObject(activeAbility.Prefab, null, spawnPosition, rotation);
    }

    protected ColliderDamageDealer[] GetColliderDamageDealers(string dealerName)
    {
        List<ColliderDamageDealer> matchingDealers = stateMachine.Enemy.DamageDealerList
            .FindAll(dealer => dealer.name.Equals(dealerName))
            .ConvertAll(dealer => dealer as ColliderDamageDealer)
            .FindAll(dealer => dealer != null);

        return matchingDealers.ToArray();
    }

    protected void SetupColliderDamageDealerAttack(string attackName, int attackIdx)
    {
        stateMachine.ReusableData.ColliderDamageDealers = GetColliderDamageDealers(attackName);

        if (stateMachine.ReusableData.ColliderDamageDealers != null)
        {
            foreach (var colliderDamageDealer in stateMachine.ReusableData.ColliderDamageDealers)
            {
                colliderDamageDealer.SetDamage(stateMachine.Enemy.Stats.DefaultData.AttackDamage * stateMachine.Enemy.Data.CombatData.AttackData[attackIdx].DamageMultiplier);
            }
        }
    }

    protected void SetupAciveAbilityDamageDealerAttack(string attackName, int attackIdx)
    {
        stateMachine.ReusableData.ActiveAbilityDamageDealer = stateMachine.Enemy.DamageDealerList.Find(dealer => dealer.name.Equals(attackName)) as ActiveAbilityDamageDealer;


        stateMachine.ReusableData.ActiveAbilityDamageDealer.SetDamage(stateMachine.Enemy.Stats.DefaultData.AttackDamage);
    }

    protected void SetupWeaponDamageDealerAttack(string attackName, int attackIdx)
    {
        stateMachine.ReusableData.WeaponDamageDealer = stateMachine.Enemy.DamageDealerList.Find(dealer => dealer.name.Equals(attackName)) as WeaponDamageDealer;

        stateMachine.ReusableData.WeaponDamageDealer.SetDamage(stateMachine.Enemy.Stats.DefaultData.AttackDamage);
    }


    #endregion
}
