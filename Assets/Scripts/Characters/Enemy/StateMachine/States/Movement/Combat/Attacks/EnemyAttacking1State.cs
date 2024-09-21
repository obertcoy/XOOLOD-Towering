using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking1State : EnemyCombatState
{

    public EnemyAttacking1State(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {

        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Rhino))
        {
            SetupColliderDamageDealerAttack("RhinoBasicAttack", 0);
        }
        else if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minions))
        {
            SetupColliderDamageDealerAttack("MinionsBasicAttack", 0);
        }
        else if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus))
        {
            SetupColliderDamageDealerAttack("PhoebusBasicAttack", 0);
        }
        else if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minotaur))
        {
            SetupWeaponDamageDealerAttack("MinotaurBasicAttack", 0);
        }
        else if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.EarthElemental) || 
            stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.IceElemental) ||
            stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.FireElemental))
        {
            SetupColliderDamageDealerAttack("ElementalBasicAttack", 0);
        }

        base.Enter();

        StartAnimation(stateMachine.Enemy.AnimationData.Attacking1ParameterHash);
    }


    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.Attacking1ParameterHash);

    }

    #endregion

    public override void StartDealDamage()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Rhino) || stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minions)
            || stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus) ||
           stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.EarthElemental) ||
            stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.IceElemental) ||
            stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.FireElemental))
        {
            foreach (ColliderDamageDealer colliderDamageDealer in stateMachine.ReusableData.ColliderDamageDealers)
            {
                colliderDamageDealer.StartDealDamage();
            }
        } else if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minotaur))
        {
            stateMachine.ReusableData.WeaponDamageDealer.StartDealDamage();
        }
    }

    public override void EndDealDamage()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Rhino) || stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minions)
            || stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus) ||
            stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.EarthElemental) ||
            stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.IceElemental) ||
            stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.FireElemental))
        {
            foreach (ColliderDamageDealer colliderDamageDealer in stateMachine.ReusableData.ColliderDamageDealers)
            {
                colliderDamageDealer.EndDealDamage();
            }
        }
        else if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Minotaur))
        {
            stateMachine.ReusableData.WeaponDamageDealer.EndDealDamage();
        }
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    #region Main Methods


    #endregion
}




