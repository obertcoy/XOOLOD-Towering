using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerAbilityRebindUI : InteractableMenu
{

    [field: Header("Ability UI")]
    [field: SerializeField] public PlayerAbilityUI AbilityUI { get; set; }

    [field: Header("Ability 1")]
    [field: SerializeField] public GameObject Ability1GameObject { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Ability1Text { get; private set; }

    [field: Header("Ability 2")]
    [field: SerializeField] public GameObject Ability2GameObject { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Ability2Text { get; private set; }

    [field: Header("Ability 3")]
    [field: SerializeField] public GameObject Ability3GameObject { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Ability3Text { get; private set; }

    [field: Header("Ability 4")]
    [field: SerializeField] public GameObject Ability4GameObject { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Ability4Text { get; private set; }

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public static PlayerAbilityRebindUI Create(Player player)
    {

        GameObject gameObject = Instantiate(GameAssets.Instance.AbilityRebindUI);

        PlayerAbilityRebindUI rebindUI = gameObject.GetComponent<PlayerAbilityRebindUI>();

        rebindUI.InitializeMenu(player);

        player.AbilitySystem.ReinitalizeAbilityUI(rebindUI.AbilityUI);

        rebindUI.Initialize();

        return rebindUI;
    }

    #region Main Methods

    private void Initialize()
    {
        Ability1Text.text = player.Input.GetAbilityActionKeyName(PlayerActiveAbilitiesControlEnum.Ability1);
        Ability2Text.text = player.Input.GetAbilityActionKeyName(PlayerActiveAbilitiesControlEnum.Ability2);
        Ability3Text.text = player.Input.GetAbilityActionKeyName(PlayerActiveAbilitiesControlEnum.Ability3);
        Ability4Text.text = player.Input.GetAbilityActionKeyName(PlayerActiveAbilitiesControlEnum.Ability4);

        InitializeAbilityButton(PlayerActiveAbilitiesControlEnum.Ability1, Ability1GameObject);
        InitializeAbilityButton(PlayerActiveAbilitiesControlEnum.Ability2, Ability2GameObject);
        InitializeAbilityButton(PlayerActiveAbilitiesControlEnum.Ability3, Ability3GameObject);
        InitializeAbilityButton(PlayerActiveAbilitiesControlEnum.Ability4, Ability4GameObject);

    }

    private void InitializeAbilityButton(PlayerActiveAbilitiesControlEnum abilityEnum, GameObject abilityGameObject)
    {

        EventTrigger eventTrigger = abilityGameObject.GetComponent<EventTrigger>();

        if (eventTrigger == null)
        {
            eventTrigger = abilityGameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { RebindAbility(abilityEnum); });

        eventTrigger.triggers.Add(entry);

    }

    public void RebindAbility(PlayerActiveAbilitiesControlEnum abilityEnum)
    {
        TextMeshProUGUI abilityText = GetAbilityText(abilityEnum);
        abilityText.text = "Waiting...";

        InputAction action = player.Input.GetAbilityAction(abilityEnum);

        if (action == null) return;

        rebindingOperation = action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithControlsExcluding("<Keyboard>/space")
            .WithControlsExcluding("<Keyboard>/escape")
            .WithControlsExcluding("<Keyboard>/w")
            .WithControlsExcluding("<Keyboard>/a")
            .WithControlsExcluding("<Keyboard>/s")
            .WithControlsExcluding("<Keyboard>/d")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(abilityEnum, abilityText))
            .Start();

    }

    

    private void RebindComplete(PlayerActiveAbilitiesControlEnum abilityEnum, TextMeshProUGUI abilityText)
    {
        rebindingOperation.Dispose();

        string newKey = player.Input.GetAbilityActionKeyName(abilityEnum);

        abilityText.text = newKey;

        player.AbilitySystem.AbilityUI.UpdateAbilityKey(abilityEnum, newKey);

        player.AbilitySystem.ReinitalizeAbilityUI(AbilityUI);
    }
    
    private TextMeshProUGUI GetAbilityText(PlayerActiveAbilitiesControlEnum control)
    {
        TextMeshProUGUI text = null;

        switch (control)
        {
            case PlayerActiveAbilitiesControlEnum.Ability1:
                text = Ability1Text;
                break;
            case PlayerActiveAbilitiesControlEnum.Ability2:
                text = Ability2Text;
                break;
            case PlayerActiveAbilitiesControlEnum.Ability3:
                text = Ability3Text;
                break;
            case PlayerActiveAbilitiesControlEnum.Ability4:
                text = Ability4Text;
                break;
        }

        return text;
    }


    #endregion

}

