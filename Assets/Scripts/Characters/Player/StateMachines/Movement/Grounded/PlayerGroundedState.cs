using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{

    private SlopeData slopeData;

    public PlayerGroundedState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        slopeData = stateMachine.Player.ColliderUtility.SlopeData;
    }

    #region IState Methods

    public override void Enter()    
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovementAnimationData.GroundedParameterHash);

        UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.MovementAnimationData.GroundedParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Float();
    }


    #endregion

    #region Main Methods

    protected void ChangeToCombatAnimation()
    {

        if (stateMachine.ReusableData.CombatToggle)
        {
            stateMachine.Player.Animator.SetLayerWeight((int)PlayerAnimationData.PlayerAnimatorLayer.Combat,
                stateMachine.Player.AnimationData.MaximumAnimationLayerWeight);
            stateMachine.Player.Animator.SetLayerWeight((int)PlayerAnimationData.PlayerAnimatorLayer.NonCombat,
                stateMachine.Player.AnimationData.MinimumAnimationLayerWeight);

            return;
        }

        stateMachine.Player.Animator.SetLayerWeight((int)PlayerAnimationData.PlayerAnimatorLayer.Combat,
                stateMachine.Player.AnimationData.MinimumAnimationLayerWeight);
        stateMachine.Player.Animator.SetLayerWeight((int)PlayerAnimationData.PlayerAnimatorLayer.NonCombat,
            stateMachine.Player.AnimationData.MaximumAnimationLayerWeight);

    }

    private void Float()
    {
        Vector3 capsuleColliderInWorldSpace = stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {

            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            float slopeSpeedModifier =  SetSlopeSpeedModiferOnAngle(groundAngle);

            if (slopeSpeedModifier == 0f) return;

            float distanceToFloatingPoint = stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;

            if (distanceToFloatingPoint == 0f) return;

            float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerYVelocity().y;

            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

            stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }

    private float SetSlopeSpeedModiferOnAngle(float angle)
    {
        float slopeSpeedModifier = movementData.SlopeSpeedAngles.Evaluate(angle);

        if(stateMachine.ReusableData.MovementOnSlopesSpeedModifier != slopeSpeedModifier)
        {
            stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }

        stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

        return slopeSpeedModifier;
    }

    private bool IsThereGroundUnderneath()
    {
        BoxCollider groundCheckCollider = stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckCollider;

        Vector3 groundColliderCenterInWorldSpace = groundCheckCollider.bounds.center;

        Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckColliderExtents, groundCheckCollider.transform.rotation, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore);

        return overlappedGroundColliders.Length > 0;        
    }

    #endregion


    #region Reusable Methods
    protected override void AddInputActionsCallbacks()
    {

        base.AddInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;
        stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
        stateMachine.Player.Input.PlayerActions.CombatToggle.started += OnCombatToggleStarted;
        stateMachine.Player.Input.PlayerActions.BasicAttack.started += OnBasicAttackStarted;

        if (!stateMachine.ReusableData.CombatToggle)
        {
            stateMachine.Player.Input.PlayerActions.Interact.started += OnInteractStarted;
        }

        stateMachine.Player.Input.PlayerActions.AbilityRebind.started += OnAbilityRebindStarted;


        if (stateMachine.ReusableData.CombatToggle)
        {
            stateMachine.Player.Input.PlayerActions.Ability1.started += OnAbility1Started;
            stateMachine.Player.Input.PlayerActions.Ability2.started += OnAbility2Started;
            stateMachine.Player.Input.PlayerActions.Ability3.started += OnAbility3Started;
            stateMachine.Player.Input.PlayerActions.Ability4.started += OnAbility4Started;
        }

    }


    protected override void RemoveInputActionsCallbacks()
    {

        base.RemoveInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;
        stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
        stateMachine.Player.Input.PlayerActions.CombatToggle.started -= OnCombatToggleStarted;
        stateMachine.Player.Input.PlayerActions.BasicAttack.started -= OnBasicAttackStarted;
        stateMachine.Player.Input.PlayerActions.Interact.started -= OnInteractStarted;
        stateMachine.Player.Input.PlayerActions.AbilityRebind.started -= OnAbilityRebindStarted;

        stateMachine.Player.Input.PlayerActions.Ability1.started -= OnAbility1Started;
        stateMachine.Player.Input.PlayerActions.Ability2.started -= OnAbility2Started;
        stateMachine.Player.Input.PlayerActions.Ability3.started -= OnAbility3Started;
        stateMachine.Player.Input.PlayerActions.Ability4.started -= OnAbility4Started;
        

    }

    protected virtual void OnMove()
    {
        if (stateMachine.ReusableData.WalkToggle)
        {
            stateMachine.ChangeState(stateMachine.WalkingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.RunningState);
    }

    protected override void OnContactWithGroundExited(Collider collider)
    {
        base.OnContactWithGroundExited(collider);

        if (IsThereGroundUnderneath()) return;

        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);

        if(!Physics.Raycast(downwardsRayFromCapsuleBottom, out _, movementData.GroundToFallRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            OnFall();
        }

    }


    protected virtual void OnFall() {
        stateMachine.ChangeState(stateMachine.FallingState);
    }


    #endregion

    #region Input Methods
  
    protected virtual void OnDashStarted(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.DashingState);
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.JumpingState);
    }

    protected virtual void OnCombatToggleStarted(InputAction.CallbackContext ctx)
    {

        if (!stateMachine.ReusableData.CombatToggle)
        {
            stateMachine.ChangeState(stateMachine.DrawSwordState);
            return;
        }

        stateMachine.ChangeState(stateMachine.SheateSwordState);
    }

    protected virtual void OnBasicAttackStarted(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.BasicAttackState);
    }

    protected virtual void OnAbility1Started(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.Player.AbilitySystem.ActiveAbilitiesStates[
            stateMachine.Player.AbilitySystem.ActiveAbilitiesControl[PlayerActiveAbilitiesControlEnum.Ability1]
            ]);
    }
    protected virtual void OnAbility2Started(InputAction.CallbackContext ctx)   
    {
        stateMachine.ChangeState(stateMachine.Player.AbilitySystem.ActiveAbilitiesStates[
            stateMachine.Player.AbilitySystem.ActiveAbilitiesControl[PlayerActiveAbilitiesControlEnum.Ability2]
            ]);
    }
    protected virtual void OnAbility3Started(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.Player.AbilitySystem.ActiveAbilitiesStates[
            stateMachine.Player.AbilitySystem.ActiveAbilitiesControl[PlayerActiveAbilitiesControlEnum.Ability3]
            ]);
    }

    protected virtual void OnAbility4Started(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.Player.AbilitySystem.ActiveAbilitiesStates[
            stateMachine.Player.AbilitySystem.ActiveAbilitiesControl[PlayerActiveAbilitiesControlEnum.Ability4]
            ]);
    }

    protected virtual void OnInteractStarted(InputAction.CallbackContext obj)
    {
        int npcLayerMask = 1 << LayerMask.NameToLayer("NPC");

        Collider[] colliders = Physics.OverlapSphere(stateMachine.Player.transform.position, stateMachine.Player.Data.GroundedData.InteractRange, npcLayerMask);

        IInteractable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            float distanceToInteractable = Vector3.Distance(stateMachine.Player.transform.position, collider.transform.position);

            if (distanceToInteractable < closestDistance)
            {
                closestDistance = distanceToInteractable;

                closestInteractable = collider.GetComponent<IInteractable>();
            }
        }

        if (closestInteractable != null)
        {
            closestInteractable.Interact(stateMachine.Player);
        }
        
    }

    private void OnAbilityRebindStarted(InputAction.CallbackContext obj)
    {
        PlayerAbilityRebindUI.Create(stateMachine.Player);
    }


    #endregion


}
