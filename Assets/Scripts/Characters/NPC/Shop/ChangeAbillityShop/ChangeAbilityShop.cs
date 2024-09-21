using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeAbilityShop : InteractableMenu
{
    [field: Header("Change Ability Shop")]
    
    [field: SerializeField] public GameObject AbilityListGameObject { get; private set; }
    [field: SerializeField] public GameObject AbilityToChangeGameObject { get; private set; }

    [field: SerializeField] public GameObject SelectedChangeAbilityGameObject { get; private set; }
    [field: SerializeField] public Image SelectedChangeAbilityLockOverlay { get; private set; }

    [field: SerializeField] public Image GoldImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Price { get; private set; }
    [field: SerializeField] public Button ChangeButton { get; private set; }
    [field: SerializeField] public Image ChangeButtonLockOverlay { get; private set; }
    [field: SerializeField] public PlayerAbilityUI PlayerAbilityUI { get; set; }

    private List<PlayerAbilitySO> unlockedAbilityList { get; set; }

    private PlayerAbilityGameObject abilityToChangeGameObject;
    private PlayerActiveAbilitySO selectedAbilityData;

    private bool locked;
    private Color originalBuyButtonColor;

    #region Public Methods
    public static ChangeAbilityShop Create(Player player)
    {

        GameObject gameObject = Instantiate(GameAssets.Instance.ChangeAbilityShopUI);

        ChangeAbilityShop changeAbilityShop = gameObject.GetComponent<ChangeAbilityShop>();

        changeAbilityShop.InitializeMenu(player);

        player.AbilitySystem.ReinitalizeAbilityUI(changeAbilityShop.PlayerAbilityUI);

        changeAbilityShop.PlayerAbilityUI.AbilityClicked += changeAbilityShop.OnSelectedAbilityToChangeClick;

        changeAbilityShop.unlockedAbilityList = player.AbilitySystem.UnlockedAbilityList;

        changeAbilityShop.Initialize();

        changeAbilityShop.GoldImage.enabled = false;
        changeAbilityShop.ChangeButtonEvent();


        return changeAbilityShop;
    }

    #endregion

    #region Main Methods

    private void Initialize()
    {

        InitializeAbility();

    }


    private void InitializeAbility()
    {
        int idx = 0;

        if (unlockedAbilityList.Count <= idx) return;

        foreach (Transform child in AbilityListGameObject.transform)
        {

            PlayerActiveAbilitySO activeAbility = unlockedAbilityList[idx] as PlayerActiveAbilitySO;

            if (activeAbility != null)
            {

                ChangeAbilityShopGameObject changeAbilityShopObject = child.GetComponentInChildren<ChangeAbilityShopGameObject>();

                changeAbilityShopObject.Initialize(this.player, activeAbility);

                EventTrigger eventTrigger = child.gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                {
                    eventTrigger = child.gameObject.AddComponent<EventTrigger>();
                }

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((data) => { OnAbilityClick(this.player, changeAbilityShopObject); });

                eventTrigger.triggers.Add(entry);
            }

            idx++;

            if (idx >= unlockedAbilityList.Count) break;
        }

    }



    #endregion

    #region Event Methods

    private void OnAbilityClick(Player player, ChangeAbilityShopGameObject clickedObject)
    {

        if (SelectedChangeAbilityGameObject != null)
        {
            SelectedChangeAbilityGameObject.GetComponent<ChangeAbilityShopGameObject>().ShowAbilityImage(clickedObject.Data);
            selectedAbilityData = clickedObject.Data;

            GoldImage.enabled = true;

            Price.color = new Color(0.97f, 0.85f, 0.15f);
            Price.text = selectedAbilityData.UnlockData.Gold.ToString();

            locked = false;

            if (player.AbilitySystem.ActiveAbilitiesControl.ContainsValue(selectedAbilityData))
            {
                Price.text = "Already in the loadout";
                Price.color = Color.gray;

                locked = true;

            }
            else if (player.Stats.RuntimeData.CurrentGold < selectedAbilityData.UnlockData.Gold)
            {
                Price.color = Color.red;

                locked = true;
            }
        }

        if (locked)
        {
            ChangeButtonLockOverlay.enabled = true;
            SelectedChangeAbilityLockOverlay.enabled = true;
        } else
        {
            ChangeButtonLockOverlay.enabled = false;
            SelectedChangeAbilityLockOverlay.enabled = false;
        }
    }

    private void OnSelectedAbilityToChangeClick(PlayerAbilityGameObject clickedObject)
    {
        if (SelectedChangeAbilityGameObject != null)
        {
            AbilityToChangeGameObject.GetComponent<ChangeAbilityShopGameObject>().ShowAbilityImage(clickedObject.Data);
            abilityToChangeGameObject = clickedObject;

        }
    }

    #endregion

    #region InteractableMenu Methods


    #endregion

    #region Event Methods

    private void ChangeButtonEvent()
    {
        EventTrigger eventTrigger = ChangeButton.gameObject.GetComponent<EventTrigger>();

        if (eventTrigger == null)
        {
            eventTrigger = ChangeButton.gameObject.AddComponent<EventTrigger>();
        }

        originalBuyButtonColor = ChangeButton.GetComponent<Image>().color;

        EventTrigger.Entry entryHover = new EventTrigger.Entry();
        entryHover.eventID = EventTriggerType.PointerEnter;
        entryHover.callback.AddListener((data) => { OnChangeButtonHover(); });
        eventTrigger.triggers.Add(entryHover);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnChangeButtonExit(); });
        eventTrigger.triggers.Add(entryExit);

        ChangeButton.onClick.AddListener(OnChangeButtonClick);

    }
    private void OnChangeButtonHover()
    {
        ChangeButton.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);

    }

    private void OnChangeButtonExit()
    {
        ChangeButton.GetComponent<Image>().color = originalBuyButtonColor;
    }

    private void OnChangeButtonClick()
    {

        if (locked)
        {
            // Display error

            return;
        } else if(abilityToChangeGameObject == null)
        {

            return;
        }

        player.ChangeActiveAbility(abilityToChangeGameObject.ControlEnum, selectedAbilityData);

        Initialize();
        
    }

    #endregion
}
