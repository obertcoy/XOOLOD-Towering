using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeAbilityShopGameObject : MonoBehaviour
{
    [field: SerializeField] public Image AbilityImage { get; private set; }
    [field: SerializeField] public Image AbilityLockOverlay { get; private set; }

    public PlayerActiveAbilitySO Data { get; private set; }

    public void Initialize(Player player, PlayerActiveAbilitySO activeAbility)
    {
        Data = activeAbility;

        AbilityImage.sprite = activeAbility.Sprite;

        if (player.AbilitySystem.ActiveAbilitiesControl.ContainsValue(activeAbility) && AbilityLockOverlay != null)
        {

            AbilityLockOverlay.enabled = true;

        }
        else
        {
            AbilityLockOverlay.enabled = false;
        }
    }

    public void ShowAbilityImage(PlayerActiveAbilitySO activeAbility)
    {

        AbilityImage.sprite = activeAbility.Sprite;

    }


    #region Event Methods

    

    #endregion

}
