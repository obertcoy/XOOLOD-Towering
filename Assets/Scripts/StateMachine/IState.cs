using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
    public void OnAnimationEnterEvent();
    public void OnAnimationExitEvent();
    public void OnAnimationTransitionEvent();
    public void StartDealDamage();
    public void EndDealDamage();
    public void TakeDamage(IDamageable source, float damage);
    public void OnTriggerEnter(Collider collider);
    public void OnTriggerExit(Collider collider);


}
