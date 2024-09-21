using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCastingState : PlayerGroundedCombat
{

    protected PlayerAbilitiesNameEnum abilityName;
    protected PlayerActiveAbilitySO currentActiveAbility;
    protected GameObject currentActiveAbilityPrefab;
    protected ActiveAbilityDamageDealer currentActiveAbilityDamageDealer;

    public PlayerCastingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        if (stateMachine.Player.AbilitySystem.GetActiveAbility(abilityName).AbilityData.CurrentCooldown > 0)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);

            return;
        }

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.CombatAnimationData.AbilityParameterHash);

        stateMachine.Player.Input.PlayerActions.Movement.Disable();

        ResetVelocity();

        currentActiveAbility = stateMachine.Player.AbilitySystem.GetUnlockedAbility(abilityName) as PlayerActiveAbilitySO;

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.CombatAnimationData.AbilityParameterHash);

        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!IsMovingHorizontally()) return;

        DecelerateHorizontally();
    }

    public override void OnAnimationEnterEvent()
    {
        currentActiveAbilityPrefab = ActivateAbility(currentActiveAbility);

        currentActiveAbilityDamageDealer = currentActiveAbilityPrefab.GetComponentInChildren<ActiveAbilityDamageDealer>();
        
        currentActiveAbilityDamageDealer.SetDamage(stateMachine.Player.Stats.RuntimeData.CurrentAttackDamage);

        stateMachine.ReusableData.SpawnAbilityAtHand = false;
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    public override void StartDealDamage()
    {
        currentActiveAbilityDamageDealer.SetSource(stateMachine.Player);

        currentActiveAbilityDamageDealer.StartDealDamage();
    }

    public override void EndDealDamage()
    {
       currentActiveAbilityDamageDealer.EndDealDamage();
    }

    #endregion

    #region Reusable methods

    protected virtual GameObject ActivateAbility(PlayerActiveAbilitySO activeAbility)
    {

        Vector3 position = stateMachine.Player.transform.position;
        Vector3 direction = stateMachine.Player.transform.forward;
        Quaternion rotation = stateMachine.Player.transform.rotation;

        Vector3 spawnPosition = position + (direction * activeAbility.SpawnDistance);

        if(stateMachine.ReusableData.SpawnAbilityAtHand) spawnPosition.y += 1f;

        if(stateMachine.Player.Stats.CheatData.CooldownModifier == 1f)
        {
            stateMachine.Player.AbilitySystem.AbilityUI.UpdateAbilityCooldownUI(activeAbility.Name);
        }

        return stateMachine.Player.InstantiateGameObject(activeAbility.Prefab, null, spawnPosition, rotation);
    }

    
    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
    }



    protected override void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;

    }


    #endregion

    #region Input methods
    private void OnMovementStarted(InputAction.CallbackContext ctx)
    {
        OnMove();
    }

    protected override void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        
    }

    protected override void OnCombatToggleStarted(InputAction.CallbackContext ctx)
    {
       
    }

    protected override void OnBasicAttackStarted(InputAction.CallbackContext ctx)
    {
        
    }

    protected override void OnDashStarted(InputAction.CallbackContext ctx)
    {
       
    }

    #endregion

    #region IDamageable Methods

    public override void TakeDamage(IDamageable source, float damage)
    {
        base.TakeDamage(source, damage);
    }

    #endregion

}
