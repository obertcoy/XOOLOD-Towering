using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityShop : InteractableMenu
{
    [field: Header("Ability Shop")]

    [field: SerializeField] public PlayerAbilityListSO AbilityList { get; private set; }
 
    [field: SerializeField] public GameObject AbilityListGameObject { get; private set; }
    [field: SerializeField] public GameObject AbilityDetailGameObject { get; private set; }

    #region Public Methods
    public static AbilityShop Create(Player player)
    {

        GameObject gameObject = Instantiate(GameAssets.Instance.AbilityShopUI);

        AbilityShop abilityShop = gameObject.GetComponent<AbilityShop>();

        abilityShop.InitializeMenu(player);

        abilityShop.Initialize();

        player.Input.SwitchInputActionMap(PlayerInputActionMapEnum.Menu);

        return abilityShop;
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

        foreach (Transform child in AbilityListGameObject.transform)
        {

            AbilityShopGameObject abilityShopObject = child.GetComponent<AbilityShopGameObject>();

            abilityShopObject.Initialize(this.player, AbilityList.ActiveAbilityList[idx]);

            EventTrigger eventTrigger = child.gameObject.GetComponent<EventTrigger>();

            if (eventTrigger == null)
            {
                eventTrigger = child.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnAbilityClick(this.player, abilityShopObject); });

            eventTrigger.triggers.Add(entry);

            idx++;

            if (idx >= AbilityList.ActiveAbilityList.Count) break;
        }
    }

 

    #endregion

    #region Event Methods

    private void OnAbilityClick(Player player, AbilityShopGameObject clickedObject)
    {

        if (AbilityDetailGameObject != null)
        {
            AbilityDetailGameObject.GetComponent<AbilityShopGameObject>().ShowAbilityDetail(player, clickedObject.Data, Initialize);
        }
    }

  



    #endregion
}
