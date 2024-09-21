using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AreaWarning : InteractableMenu
{
    [field: Header("Warning UI")]

    [field: SerializeField] private TextMeshProUGUI SceneName;
    [field: SerializeField] private TextMeshProUGUI RecommendedLevel;
    [field: SerializeField] private Button EnterButton;

    public delegate void OnEnterButtonClickedCallback();
    private Color originalEnterButtonColor;

    public static AreaWarning Create(Player player, string sceneName, int recMinlevel, int recMaxLevel, OnEnterButtonClickedCallback callback)
    {
        GameObject gameObject = Instantiate(GameAssets.Instance.AreaWarning);

        AreaWarning areaWarning = gameObject.GetComponent<AreaWarning>();

        areaWarning.InitializeMenu(player);

        areaWarning.Initialize(sceneName, recMinlevel, recMaxLevel);

        areaWarning.EnterButtonEvent(callback);

        player.Input.SwitchInputActionMap(PlayerInputActionMapEnum.Menu);

        return areaWarning;
    }

    #region Main Methods

    private void Initialize(string sceneName, int recMinlevel, int recMaxLevel)
    {

        SceneName.text = sceneName;
        RecommendedLevel.text = "(Recommended Level: " + recMinlevel + "-" + recMaxLevel + ")";

    }

    private void EnterButtonEvent(OnEnterButtonClickedCallback callback)
    {
        Debug.Log("Before accessing EnterButton");
        originalEnterButtonColor = EnterButton.GetComponent<Image>().color;
        Debug.Log("After accessing EnterButton");

        EventTrigger enterButtonEventTrigger = EnterButton.GetComponent<EventTrigger>();

        EnterButton.onClick.AddListener(() => {

            CloseMenu();
            callback?.Invoke();
            
        });

        Debug.Log("After accessing EnterButton 2");


        //EventTrigger.Entry entryHover = new EventTrigger.Entry();
        //entryHover.eventID = EventTriggerType.PointerEnter;
        //entryHover.callback.AddListener((data) => { OnEnterButtonHover(); });
        //enterButtonEventTrigger.triggers.Add(entryHover);

        //Debug.Log("After accessing EnterButton 3");

        //EventTrigger.Entry entryExit = new EventTrigger.Entry();
        //entryExit.eventID = EventTriggerType.PointerExit;
        //entryExit.callback.AddListener((data) => { OnButtonEnterExit(); });
        //enterButtonEventTrigger.triggers.Add(entryExit);

    

    }

    private void OnEnterButtonHover()
    {
        EnterButton.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
    }
    private void OnButtonEnterExit()
    {
        EnterButton.GetComponent<Image>().color = originalEnterButtonColor;
    }

    #endregion

}
