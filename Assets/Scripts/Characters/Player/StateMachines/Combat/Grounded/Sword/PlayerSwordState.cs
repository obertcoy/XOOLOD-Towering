using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwordState : PlayerGroundedCombat
{
    protected PlayerSwordData swordData;
    public PlayerSwordState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        swordData = stateMachine.Player.Data.CombatData.SwordData;
    }

    #region IState Methods
    public override void Enter()
    {

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        stateMachine.ReusableData.CombatToggle = !stateMachine.ReusableData.CombatToggle;

        stateMachine.Player.Input.PlayerActions.Movement.Disable();
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }

    public override void OnAnimationExitEvent()
    {
        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    #endregion

    #region Main Methods

    protected void DrawWeapon()
    {

        stateMachine.Player.DestroyObject(stateMachine.ReusableData.CurrentWeaponInHand);

        stateMachine.ReusableData.CurrentWeaponInHand = stateMachine.Player.InstantiateGameObject(
            GameAssets.Instance.Sword, stateMachine.Player.Data.CombatData.SwordData.SwordHolder.transform);

        stateMachine.ReusableData.WeaponDamageDealer = stateMachine.ReusableData.CurrentWeaponInHand.GetComponentInChildren<WeaponDamageDealer>();
        stateMachine.ReusableData.CurrentWeaponInHand.GetComponentInChildren<TrailRenderer>().emitting = false;

    }

    protected void SheathSword()
    {
        stateMachine.Player.DestroyObject(stateMachine.ReusableData.CurrentWeaponInHand);

        stateMachine.ReusableData.CurrentWeaponInHand = stateMachine.Player.InstantiateGameObject(
            GameAssets.Instance.Sword, stateMachine.Player.Data.CombatData.SwordData.SheathHolder.transform);

        stateMachine.ReusableData.WeaponDamageDealer = stateMachine.ReusableData.CurrentWeaponInHand.GetComponentInChildren<WeaponDamageDealer>();
        stateMachine.ReusableData.CurrentWeaponInHand.GetComponentInChildren<TrailRenderer>().emitting = false;

    }

    #endregion

    #region Input Methods


    #endregion


}
