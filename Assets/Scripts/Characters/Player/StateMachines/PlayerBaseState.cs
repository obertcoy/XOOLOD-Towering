using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{

    protected PlayerStateMachine stateMachine;


    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;


    }

   

    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);

        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnAnimationEnterEvent()
    {
    }

    public virtual void OnAnimationExitEvent()
    {
    }

    public virtual void OnAnimationTransitionEvent()
    {
    }

    public virtual void StartDealDamage()
    {
    }

    public virtual void EndDealDamage()
    {
    }

    public virtual void TakeDamage(IDamageable source, float damage)
    {

    }


    public virtual void OnTriggerEnter(Collider collider)
    {
        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer)){
            
            OnContactWithGround(collider);
            return;
        }
    }

    public void OnTriggerExit(Collider collider)
    {

        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
        {
            OnContactWithGroundExited(collider);

            return;
        }

    }

    #region Main Methods

    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        return directionAngle;
    }
    
    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
        stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
    }

    private float AddCameraRotationToAngle(float angle)
    {
        angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        if (angle != stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(angle);
        }

        return angle;
    }

    #endregion

    #region Reusable Methods

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected void StartTriggerAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetTrigger(animationHash);
    }

   

    protected float UpdateTargetRotation(Vector3 direction, bool rotateCamera = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (rotateCamera)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }


        return directionAngle;
    }



    protected void RotateTowardsTargetRotation()
    {
        float currYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

        if (currYAngle == stateMachine.ReusableData.CurrentTargetRotation.y) return;

        float smooothedYAngle = Mathf.SmoothDampAngle(currYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);
        stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smooothedYAngle, 0f);
        stateMachine.Player.Rigidbody.MoveRotation(targetRotation);

    }
    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }


    protected virtual void AddInputActionsCallbacks()
    {
   
    }

   

    protected virtual void RemoveInputActionsCallbacks()
    {
   
    }

    protected virtual void OnContactWithGround(Collider collider)
    {
    }

    protected virtual void OnContactWithGroundExited(Collider collider)
    {
    }




    #endregion

    #region Input Methods


    #endregion
}
