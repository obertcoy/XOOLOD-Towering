using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseState : IState
{

    protected NPCStateMachine stateMachine;


    public NPCBaseState(NPCStateMachine npcStateMachine)
    {
        stateMachine = npcStateMachine;
    }

    #region IState Methods
    public virtual void Enter()
    {
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
        stateMachine.NPC.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        stateMachine.NPC.Animator.SetBool(animationHash, false);
    }

    protected void StartTriggerAnimation(int animationHash)
    {
        stateMachine.NPC.Animator.SetTrigger(animationHash);
    }

   

    #endregion

}