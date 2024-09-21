using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventTrigger : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    public void TriggerOnMovementStateAnimationEnterEvent()
    {
        if (IsInAnimationTransation()) return;

        player.OnMovementStateAnimationEnterEvent();
    }
    public void TriggerOnMovementStateAnimationExitEvent()
    {
        if (IsInAnimationTransation()) return;

        player.OnMovementStateAnimationExitEvent();
    }
    public void TriggerOnMovementStateAnimationTransitionEvent()
    {
        if (IsInAnimationTransation()) return;

        player.OnMovementStateAnimationTransitionEvent();
    }

    public void StartDealDamage()
    {
        player.StartDealDamage();
    }

    public void EndDealDamage()
    {
        player.EndDealDamage();
    }

    private bool IsInAnimationTransation(int layerIndex = 0)
    {
        return player.Animator.IsInTransition(layerIndex);
    }
}
