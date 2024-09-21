using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingRedEnergyExplosionState : PlayerCastingState
{
    public PlayerCastingRedEnergyExplosionState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        abilityName = PlayerAbilitiesNameEnum.RedEnergyExplosion;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.RedEnergyExplosionParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.RedEnergyExplosionParameterHash);
    }

    #endregion

    public override void OnAnimationEnterEvent()
    {
        base.OnAnimationEnterEvent();

        stateMachine.ReusableData.OngoingAbilityPrefab = currentActiveAbilityPrefab.gameObject;
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();

        stateMachine.Player.DestroyObject(stateMachine.ReusableData.OngoingAbilityPrefab, 2.2f);

    }

}
