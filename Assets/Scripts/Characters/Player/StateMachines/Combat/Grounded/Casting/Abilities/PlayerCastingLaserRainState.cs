using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingLaserRainState : PlayerCastingState
{

    public PlayerCastingLaserRainState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }


    #region IState Methods

    public override void Enter()
    {
        abilityName = PlayerAbilitiesNameEnum.LaserRain;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.LaserRainParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.LaserRainParameterHash);
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
