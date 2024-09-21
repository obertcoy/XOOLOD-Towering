using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingMeteorShowerState : PlayerCastingState
{

    public PlayerCastingMeteorShowerState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        abilityName = PlayerAbilitiesNameEnum.MeteorShower;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.MeteorShowerParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.MeteorShowerParameterHash);
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

        stateMachine.Player.DestroyObject(stateMachine.ReusableData.OngoingAbilityPrefab, 5.2f);

    }
}
