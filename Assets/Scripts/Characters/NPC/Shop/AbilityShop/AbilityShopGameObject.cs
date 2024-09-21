using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class AbilityShopGameObject : MonoBehaviour
{
    [field: SerializeField] public Image AbilityImage { get; private set; }
    [field: SerializeField] public Image AbilityLockOverlay { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Name { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Description { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Detail { get; private set; }
    [field: SerializeField] public Image GoldImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Price { get; private set; }
    [field: SerializeField] public Button BuyButton { get; private set; }
    [field: SerializeField] public Image BuyButtonLockOverlay { get; private set; }

    public PlayerActiveAbilitySO Data { get; private set; }

    private Player player;

    private bool locked;
    private Color originalBuyButtonColor;

    private Action onAbilityBoughtCallback;

    private void Start()
    {
        if (BuyButton != null)
        {
            BuyButtonEvent();
        }

        if(GoldImage != null)
        {
            GoldImage.enabled = false;
        }
    }

    public void Initialize(Player player, PlayerActiveAbilitySO activeAbility)
    {
        Data = activeAbility;

        AbilityImage.sprite = activeAbility.Sprite;


        if(player.AbilitySystem.GetUnlockedAbility(activeAbility.Name) != null && AbilityLockOverlay != null)
        {

            AbilityLockOverlay.enabled = true;

        } else
        {
            AbilityLockOverlay.enabled = false;
        }
    }

    public void ShowAbilityDetail(Player player, PlayerActiveAbilitySO activeAbility, Action onAbilityBoughtCallback)
    {
        locked = false;

        this.player = player;

        Data = activeAbility;

        this.onAbilityBoughtCallback = onAbilityBoughtCallback;

        AbilityImage.sprite = activeAbility.Sprite;
        Name.text = activeAbility.DisplayName;
        Description.text = "Description:";

        Detail.text = activeAbility.Description;
        Detail.fontSize = 8;
        Detail.alignment = TextAlignmentOptions.TopLeft;

        GoldImage.enabled = true;

        Price.color = new Color(0.97f, 0.85f, 0.15f);
        Price.text = activeAbility.UnlockData.Gold.ToString();

        BuyButtonLockOverlay.enabled = false;

        if (player.AbilitySystem.GetUnlockedAbility(activeAbility.Name) != null)
        {
            Price.text = "Already Unlocked";
            Price.color = Color.gray;

            BuyButtonLockOverlay.enabled = true;
            locked = true;

        }
        else if (player.Stats.RuntimeData.CurrentGold < activeAbility.UnlockData.Gold)
        {
            Price.color = Color.red;

            BuyButtonLockOverlay.enabled = true;
            locked = true;
        }

    }

    #region Event Methods

    private void BuyButtonEvent()
    {
        EventTrigger eventTrigger = BuyButton.gameObject.GetComponent<EventTrigger>();

        if (eventTrigger == null)
        {
            eventTrigger = BuyButton.gameObject.AddComponent<EventTrigger>();
        }

        originalBuyButtonColor = BuyButton.GetComponent<Image>().color;

        EventTrigger.Entry entryHover = new EventTrigger.Entry();
        entryHover.eventID = EventTriggerType.PointerEnter;
        entryHover.callback.AddListener((data) => { OnBuyButtonHover(); });
        eventTrigger.triggers.Add(entryHover);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnBuyButtonExit(); });
        eventTrigger.triggers.Add(entryExit);

        BuyButton.onClick.AddListener(OnButtonClick);

    }
    private void OnBuyButtonHover()
    {
       BuyButton.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);

    }

    private void OnBuyButtonExit()
    {
        BuyButton.GetComponent<Image>().color = originalBuyButtonColor;
    }

    private void OnButtonClick()
    {

        if (locked)
        {
            // Display error

            return;
        }


        player.UnlockAbility(Data);

        onAbilityBoughtCallback?.Invoke();

        ShowAbilityDetail(player, Data, onAbilityBoughtCallback);
    }

    #endregion


}
