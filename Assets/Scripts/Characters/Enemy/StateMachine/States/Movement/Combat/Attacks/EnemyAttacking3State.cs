using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking3State : EnemyCombatState
{
    public EnemyAttacking3State(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus) && !stateMachine.ReusableData.isAirborne)
        {
            stateMachine.ReusableData.NextDesiredState = stateMachine.Attacking3State;

            stateMachine.ChangeState(stateMachine.TakingOffState);

        } else
        {
            base.Enter();

            if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus))
            {

            }

            stateMachine.ReusableData.CurrentAttack3Cooldown = stateMachine.Enemy.Data.CombatData.AttackData[2].AttackCooldown;

            StartAnimation(stateMachine.Enemy.AnimationData.Attacking3ParameterHash);
        }


    }


    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.AnimationData.Attacking3ParameterHash);

    }

    public override void StartDealDamage()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus)){

            stateMachine.ReusableData.ActiveAbilityDamageDealer.StartDealDamage();

        }

    }

    public override void EndDealDamage()
    {

        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus))
        {

            stateMachine.ReusableData.ActiveAbilityDamageDealer.EndDealDamage();

        }

    }

    public override void OnAnimationEnterEvent()
    {
        Debug.Log("Dragon breath ability: " + stateMachine.Enemy.AbilityList.GetAbility(EnemyAbilitiesNameEnum.DragonBreath));

        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus))
        {
            GameObject dragonBreath = GameObject.FindGameObjectWithTag("DragonBreath");

            EnemyActiveAbilitySO ability = (stateMachine.Enemy.AbilityList.GetAbility(EnemyAbilitiesNameEnum.DragonBreath) as EnemyActiveAbilitySO);

            ability.ActivateSetupAbility(dragonBreath);

            stateMachine.ReusableData.ActiveAbilityDamageDealer = dragonBreath.GetComponentInChildren<ActiveAbilityDamageDealer>();

            stateMachine.ReusableData.ActiveAbilityDamageDealer.SetDamage(stateMachine.Enemy.Stats.DefaultData.AttackDamage);


        }

    }

    public override void OnAnimationExitEvent()
    {
        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus))
        {

            EnemyActiveAbilitySO ability = (stateMachine.Enemy.AbilityList.GetAbility(EnemyAbilitiesNameEnum.DragonBreath) as EnemyActiveAbilitySO);

            ability.DeactivateSetupAbility();
        }
            
    }

    public override void OnAnimationTransitionEvent()
    {

        if (stateMachine.Enemy.Data.Name.Equals(EnemyNameEnum.Phoebus))
        {
            stateMachine.ChangeState(stateMachine.LandingState);
        }
    }

    #endregion

}
