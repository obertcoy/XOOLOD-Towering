using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActiveAbilitiesControlEnum
{
    Ability1,
    Ability2,
    Ability3,
    Ability4
}

[Serializable]
public class PlayerAbilitySystem
{

    [field: Header("Abilities")]

    [field: SerializeField] public List<PlayerAbilitySO> UnlockedAbilityList { get; set; }
    public Dictionary<PlayerActiveAbilitiesControlEnum, PlayerActiveAbilitySO> ActiveAbilitiesControl { get; set; }
    public Dictionary<PlayerActiveAbilitySO, PlayerCastingState> ActiveAbilitiesStates { get; set; }
    [field: SerializeField] public PlayerAbilityUI AbilityUI { get; set; }

    private Player player;

    #region Main Methods

    public void Initialize(Player player)
    {
        // UnlockedAbilityList = new List<PlayerAbilitySO>();
        ActiveAbilitiesControl = new Dictionary<PlayerActiveAbilitiesControlEnum, PlayerActiveAbilitySO>();
        ActiveAbilitiesStates = new Dictionary<PlayerActiveAbilitySO, PlayerCastingState>();
        AbilityUI.Initialize();

        this.player = player;
    }

    public PlayerAbilitySO GetUnlockedAbility(PlayerAbilitiesNameEnum name)
    {

        PlayerAbilitySO _ability = null;

        foreach (PlayerAbilitySO ability in UnlockedAbilityList)
        {
            if (name.Equals(ability.Name))
            {
                _ability = ability;
                break;
            }
        }

        return _ability;
    }

    public PlayerActiveAbilitySO GetActiveAbility(PlayerAbilitiesNameEnum name)
    {

        PlayerActiveAbilitySO _ability = null;

        foreach (PlayerActiveAbilitySO ability in UnlockedAbilityList)
        {
            if (name.Equals(ability.Name))
            {
                _ability = ability;
                break;
            }
        }

        return _ability;
    }


    public void UnlockAbility(PlayerStateMachine stateMachine, PlayerAbilitySO ability)
    {
        ability.UnlockAbility();

        if (!UnlockedAbilityList.Contains(ability)) UnlockedAbilityList.Add(ability);

        if (ability is PlayerActiveAbilitySO activeAbility)
        {

            AddToEmptyActiveAbilitiesSlot(activeAbility);
            AddAbilitiesState(stateMachine, activeAbility);
        }
    }

    public void ChangeActiveAbility(PlayerActiveAbilitiesControlEnum control, PlayerActiveAbilitySO activeAbility)
    {
        ActiveAbilitiesControl[control] = activeAbility;
        AbilityUI.UpdateAbilityUI(control, activeAbility, player.Input.GetAbilityActionKeyName(control));
    }

    public void ReinitalizeAbilityUI(PlayerAbilityUI abilityUI)
    {
        abilityUI.Initialize();

        foreach (PlayerActiveAbilitiesControlEnum control in Enum.GetValues(typeof(PlayerActiveAbilitiesControlEnum)))
        {
            if (ActiveAbilitiesControl.TryGetValue(control, out PlayerActiveAbilitySO activeAbility))
            {
                abilityUI.UpdateAbilityUI(control, activeAbility, player.Input.GetAbilityActionKeyName(control));
            }
        }
    }

    #endregion

    #region Private Methods

    private void AddToEmptyActiveAbilitiesSlot(PlayerActiveAbilitySO activeAbility)
    {

        foreach (PlayerActiveAbilitiesControlEnum control in Enum.GetValues(typeof(PlayerActiveAbilitiesControlEnum)))
        {

            if (!ActiveAbilitiesControl.ContainsKey(control))
            {
                ActiveAbilitiesControl.Add(control, activeAbility);
                AbilityUI.UpdateAbilityUI(control, activeAbility, player.Input.GetAbilityActionKeyName(control));

                break;
            }
        }
    }


    private void AddAbilitiesState(PlayerStateMachine stateMachine, PlayerActiveAbilitySO activeAbility)
    {
        PlayerCastingState state = null;

        switch (activeAbility.Name)
        {

            case PlayerAbilitiesNameEnum.HollowRed:
                {
                    state = stateMachine.CastingHollowRedState;
                    break;
                }
            case PlayerAbilitiesNameEnum.HorizontalSlash:
                {
                    state = stateMachine.CastingHorizontalState;
                    break;
                }
            case PlayerAbilitiesNameEnum.LaserRain:
                {
                    state = stateMachine.CastingLaserRainState;
                    break;
                }
            case PlayerAbilitiesNameEnum.MeteorShower:
                {
                    state = stateMachine.CastingMeteorShowerState;
                    break;
                }
            case PlayerAbilitiesNameEnum.RedEnergyExplosion:
                {
                    state = stateMachine.RedEnergyExplosionState;
                    break;
                }
        }

        if (state != null) ActiveAbilitiesStates.Add(activeAbility, state);

    }

    #endregion

}
