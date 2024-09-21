using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingHorizontalSlashState : PlayerCastingState
{
    public PlayerCastingHorizontalSlashState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        abilityName = PlayerAbilitiesNameEnum.HorizontalSlash;

        base.Enter();

        stateMachine.ReusableData.SpawnAbilityAtHand = true;

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.HorizontalSlashParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.HorizontalSlashParameterHash);
    }

}
