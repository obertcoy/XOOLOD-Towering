using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerInputActionMapEnum
{
    Player,
    Menu
}

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }
    public PlayerInputActions.MenuActions MenuActions { get; private set; }

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player;
        MenuActions = InputActions.Menu;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    public void SwitchInputActionMap(PlayerInputActionMapEnum map)
    {
        InputActions.Disable();

        switch (map)
        {
            case PlayerInputActionMapEnum.Player:
                PlayerActions.Enable();
                break;

            case PlayerInputActionMapEnum.Menu:
                MenuActions.Enable();
                break;

        }

    }
    public void DisableActionFor(InputAction action, float seconds)
    {
        StartCoroutine(DisableAction(action, seconds));
    }

    private IEnumerator DisableAction(InputAction action, float seconds)
    {
        action.Disable();

        yield return new WaitForSeconds(seconds);

        action.Enable();
    }

    public InputAction GetAbilityAction(PlayerActiveAbilitiesControlEnum abilityEnum)
    {
        InputAction action = null;

        switch (abilityEnum)
        {
            case PlayerActiveAbilitiesControlEnum.Ability1:
                action = PlayerActions.Ability1;
                break;
            case PlayerActiveAbilitiesControlEnum.Ability2:
                action = PlayerActions.Ability2;
                break;
            case PlayerActiveAbilitiesControlEnum.Ability3:
                action = PlayerActions.Ability3;
                break;
            case PlayerActiveAbilitiesControlEnum.Ability4:
                action = PlayerActions.Ability4;
                break;
        }

        return action;
    }

    public string GetAbilityActionKeyName(PlayerActiveAbilitiesControlEnum abilityEnum)
    {
        InputAction action = GetAbilityAction(abilityEnum);

        return InputControlPath.ToHumanReadableString(
            action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void RebindAbility(PlayerActiveAbilitiesControlEnum abilityEnum)
    {

        InputAction action = GetAbilityAction(abilityEnum);

        if (action == null) return;

        rebindingOperation = action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();

    }

    private void RebindComplete()
    {
        rebindingOperation.Dispose();
    }

}
