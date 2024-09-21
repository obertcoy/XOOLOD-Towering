using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoroutineManager : MonoBehaviour
{
    private Player player;

    public PlayerCoroutineManager(Player player)
    {
        this.player = player;
    }

    public void Initialize(Player player)
    {
        this.player = player;
    }

    private IEnumerator ReduceAbilityCooldownCoroutine()
    {
        foreach (PlayerAbilitySO ability in player.AbilitySystem.ActiveAbilitiesControl.Values)
        {
            
            yield return StartCoroutine(((PlayerActiveAbilitySO)ability).ReduceCooldownCoroutine());
            
        }
    }




}
