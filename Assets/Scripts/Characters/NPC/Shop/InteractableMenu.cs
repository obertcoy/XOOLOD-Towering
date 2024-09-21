using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractableMenu : MonoBehaviour
{
    [field: Header("Menu")]

    [field: SerializeField] public GameObject CloseIcon { get; private set; }

    protected Player player { get; private set; }

    private Color originalCloseIconColor;

    private GameObject playerCamera;


    #region Main Methods
    public void InitializeMenu(Player player)
    {
        this.player = player;

        player.Input.SwitchInputActionMap(PlayerInputActionMapEnum.Menu);

        if(CloseIcon != null)
        {
            CloseIconEvent();
        }

        AddInputActionsCallbacks();

        playerCamera = GameObject.FindWithTag("PlayerCamera");

        if (playerCamera != null) playerCamera.SetActive(false);
    }

    #endregion

    #region Reusable Methods

    protected virtual void CloseMenu()
    {
        RemoveInputActionsCallbacks();

        player.Input.SwitchInputActionMap(PlayerInputActionMapEnum.Player);

        if (playerCamera != null) playerCamera.SetActive(true);

        Destroy(gameObject);
    }

    protected virtual void AddInputActionsCallbacks()
    {

        player.Input.MenuActions.Close.started += OnCloseStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {

        player.Input.MenuActions.Close.started -= OnCloseStarted;
    }

    #endregion

    #region Event Methods
    private void CloseIconEvent()
    {
        originalCloseIconColor = CloseIcon.GetComponent<Image>().color;

        EventTrigger closeIconEventTrigger = CloseIcon.GetComponent<EventTrigger>();

        if (closeIconEventTrigger == null)
        {
            closeIconEventTrigger = CloseIcon.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entryHover = new EventTrigger.Entry();
        entryHover.eventID = EventTriggerType.PointerEnter;
        entryHover.callback.AddListener((data) => { OnCloseIconHover(); });
        closeIconEventTrigger.triggers.Add(entryHover);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnCloseIconExit(); });
        closeIconEventTrigger.triggers.Add(entryExit);

        EventTrigger.Entry entryClickIcon = new EventTrigger.Entry();
        entryClickIcon.eventID = EventTriggerType.PointerClick;
        entryClickIcon.callback.AddListener((data) => { OnCloseIconClick(); });
        closeIconEventTrigger.triggers.Add(entryClickIcon);


    }

    private void OnCloseIconHover()
    {
        CloseIcon.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
    }
    private void OnCloseIconExit()
    {
        CloseIcon.GetComponent<Image>().color = originalCloseIconColor;
    }

    private void OnCloseIconClick()
    {
        CloseMenu();
    }

    #endregion

    #region Input Methods

    private void OnCloseStarted(InputAction.CallbackContext obj)
    {
        CloseMenu();
    }

    
    #endregion


}
