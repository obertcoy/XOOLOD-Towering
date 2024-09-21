using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingHollowRedState : PlayerCastingState
{
    public PlayerCastingHollowRedState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        abilityName = PlayerAbilitiesNameEnum.HollowRed;

        base.Enter();

        stateMachine.ReusableData.SpawnAbilityAtHand = true;

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.HollowRedParamaterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.HollowRedParamaterHash);
    }

    #endregion

    public override void OnAnimationEnterEvent()
    {
        base.OnAnimationEnterEvent();

        Transform child = currentActiveAbilityPrefab.transform.Find("Collider");

        child.transform.localPosition = Vector3.zero;
        
    }
}
