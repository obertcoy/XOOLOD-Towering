using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerAbilityGameObject: MonoBehaviour
{
    [field: SerializeField] public PlayerActiveAbilitiesControlEnum ControlEnum { get; private set; }
    [field: SerializeField] public Image AbilityImage { get; private set; }
    [field: SerializeField] public Image AbilityCooldownOverlay { get; private set; }
    [field: SerializeField] public TextMeshProUGUI CooldownValueText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Key { get; private set; }
    public PlayerActiveAbilitySO Data { get; private set; }


    public void Initialize(PlayerActiveAbilitiesControlEnum control, PlayerActiveAbilitySO activeAbility, string key)
    {
        ControlEnum = control;

        Data = activeAbility;

        AbilityImage.sprite = activeAbility.Sprite;

        Key.text = key;

        Data.AbilityData.CurrentCooldown = 0;
    }

    public void UpdateAbilityKey(string key)
    {
        Key.text = key;
    }
    
    public void ActivateAbility()
    {
        Data.AbilityData.CurrentCooldown = Data.AbilityData.Cooldown;
    }

    public void Update()
    {
        if (Data is null) return;

        if (Data.AbilityData.CurrentCooldown <= 0)
        {
            CooldownValueText.text = "";
            AbilityCooldownOverlay.fillAmount = 0;

            return;
        }

        Data.AbilityData.CurrentCooldown -= Time.deltaTime;

        StartCoroutine(CooldownCoroutine());
    }
 
    private IEnumerator CooldownCoroutine()
    {
        yield return null;

        CooldownValueText.text = Data.AbilityData.CurrentCooldown.ToString("0.0") + " s";

        UpdateCooldownOverlay();
    }
    private void UpdateCooldownOverlay()
    {
        if (AbilityCooldownOverlay != null)
        {
            float fillAmount = 1 - Mathf.Clamp01(Data.AbilityData.CurrentCooldown / Data.AbilityData.Cooldown);
            AbilityCooldownOverlay.fillAmount = fillAmount;
            
        }
    }
}
