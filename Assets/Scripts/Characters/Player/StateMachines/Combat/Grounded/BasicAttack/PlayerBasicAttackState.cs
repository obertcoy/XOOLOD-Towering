using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicAttackState : PlayerGroundedCombat
{
    PlayerBasicAttackData basicAttackData;
    protected int currentBasicAttackCombo;
    protected bool attack;
    protected bool canTransition;
    private GameObject slashVfx;

    public PlayerBasicAttackState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        basicAttackData = stateMachine.Player.Data.CombatData.BasicAttackData;
        
    }

    #region IState Methods

    public override void Enter()
    {

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        if (!stateMachine.ReusableData.CombatToggle)
        {
            //stateMachine.ChangeState(stateMachine.DrawSwordState);
            return;
        }

        stateMachine.Player.Input.PlayerActions.Movement.Disable();

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.BasicAttackParameterHash);

        BasicAttack();

    }


    public override void HandleInput()
    {

        if (stateMachine.Player.Input.PlayerActions.BasicAttack.triggered)
        {
            attack = true;
        }
    }

    public override void Update()
    {

        if (canTransition && attack)
        {
            BasicAttack();
            return;
        }

    }

    public override void Exit()
    {

        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.BasicAttackParameterHash);

        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    
    }

    public override void OnAnimationExitEvent()
    {
        canTransition = true;
    }

    public override void OnAnimationTransitionEvent()
    {
        if(!attack || currentBasicAttackCombo == basicAttackData.BasicAttackComboLimit) stateMachine.ChangeState(stateMachine.IdlingState);
    }

    public override void StartDealDamage()
    {
        stateMachine.ReusableData.WeaponDamageDealer.StartDealDamage();
        stateMachine.ReusableData.CurrentWeaponInHand.GetComponentInChildren<TrailRenderer>().emitting = true;

    }

    public override void EndDealDamage()
    {
        stateMachine.ReusableData.WeaponDamageDealer.EndDealDamage();
        stateMachine.ReusableData.CurrentWeaponInHand.GetComponentInChildren<TrailRenderer>().emitting = false;

    }


    #endregion

    #region Main Methods

    private void Reset()
    {
        attack = false;
        canTransition = false;
    }

    private void BasicAttack() {


        if (currentBasicAttackCombo == basicAttackData.BasicAttackComboLimit)
        {
            currentBasicAttackCombo = 0;

            stateMachine.ChangeState(stateMachine.IdlingState);
            return;
        }

        ++currentBasicAttackCombo;

        stateMachine.ReusableData.WeaponDamageDealer.SetDamage(stateMachine.Player.Stats.RuntimeData.CurrentAttackDamage * Mathf.Sqrt(currentBasicAttackCombo)); 

        Reset();

        StartTriggerAnimation(stateMachine.Player.AnimationData.CombatAnimationData.BasicAttackTriggerParameterHash);
      
    }


    #endregion


    #region Reusable Methods

    #endregion

    #region Input Methods

    protected override void OnBasicAttackStarted(InputAction.CallbackContext ctx)
    {
     
    }

    #endregion


}
