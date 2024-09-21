using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking2State : EnemyCombatState
{
    public EnemyAttacking2State(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {

        base.Enter();

        stateMachine.ReusableData.CurrentAttack2Cooldown = stateMachine.Enemy.Data.CombatData.AttackData[1].AttackCooldown;

        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minotaur))
        {
            SetupColliderDamageDealerAttack("MinotaurKickAttack", 1);
        }

        StartAnimation(stateMachine.Enemy.AnimationData.Attacking2ParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.Attacking2ParameterHash);

    }

    public override void StartDealDamage()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minotaur))
        {
            foreach (ColliderDamageDealer colliderDamageDealer in stateMachine.ReusableData.ColliderDamageDealers)
            {
                colliderDamageDealer.StartDealDamage();
            }
        }

    }

    public override void EndDealDamage()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minotaur))
        {
            foreach (ColliderDamageDealer colliderDamageDealer in stateMachine.ReusableData.ColliderDamageDealers)
            {
                colliderDamageDealer.EndDealDamage();
            }
        }

    }

    public override void OnAnimationEnterEvent()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus))
        {
            ActivateAbility(stateMachine.Enemy.AbilityList.GetAbility(EnemyAbilitiesNameEnum.SpawnMinions) as EnemyActiveAbilitySO);
        }
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    #endregion
}
