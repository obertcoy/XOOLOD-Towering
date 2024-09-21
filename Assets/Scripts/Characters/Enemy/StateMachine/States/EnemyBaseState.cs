using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{

    protected EnemyStateMachine stateMachine;

    
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    #region IState Methods
    public virtual void Enter()
    {
        Debug.Log("Enemy State: " + GetType().Name);

    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
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

    public virtual void OnTriggerEnter(Collider collider)
    {
    }

    public virtual void OnTriggerExit(Collider collider)
    {
    }

    public virtual void PhysicsUpdate()
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

    #endregion

    #region Reusable Methods

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    protected void StartTriggerAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetTrigger(animationHash);
    }

    #endregion

}
