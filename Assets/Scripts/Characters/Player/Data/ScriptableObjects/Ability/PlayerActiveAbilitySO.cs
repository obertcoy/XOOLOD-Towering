using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Active", menuName = "Custom/Characters/Ability/Player/Active")]
public class PlayerActiveAbilitySO : PlayerAbilitySO
{
    [field: Header("Active Ability")]
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public PlayerActiveAbilityData AbilityData { get; set; }
    [field: SerializeField] public float SpawnDistance { get; private set; } = 3f;

    [field: SerializeField] public bool Moving { get; private set; }

    [field: Header("Moving")]
    [field: SerializeField] public float MovingSpeed { get; private set; } = 5f;
    [field: SerializeField] public float MaxDistance { get; private set; } = 200f;


     
    [field: Header("Hotkey")]
    [field: SerializeField] public InputActionReference InputAction { get; private set; }

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    
    public void ActivateAbility()
    {
        AbilityData.CurrentCooldown = AbilityData.Cooldown;
    }

    public IEnumerator ReduceCooldownCoroutine()
    {

        while(AbilityData.CurrentCooldown > 0)
        {
            AbilityData.CurrentCooldown -= 1;

            yield return new WaitForSeconds(1f);
        }

    }

    public void RebindKey()
    {
        rebindOperation = InputAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse/position")
            .WithControlsExcluding("Mouse/delta")
            .WithControlsExcluding("Keyboard/escape")
            .WithControlsExcluding("Keyboard/leftShift")
            .WithControlsExcluding("Keyboard/leftCtrl")
            .WithControlsExcluding("Keyboard/r")
            .OnComplete(operation => RebindCompleted());


        rebindOperation.Start();
    }

    public void RebindCompleted()
    {
        rebindOperation.Dispose();
    } 
}
