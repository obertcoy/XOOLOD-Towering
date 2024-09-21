using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerRuntimeStatsUI
{
    
    [field: Header("Health UI")]

    [field: SerializeField] public Image HealthOrbFill { get; private set; }
    [field: SerializeField] public TextMeshProUGUI CurrentHealth { get; private set; }

    [field: Header("Experience UI")]

    [field: SerializeField] public Slider ExperienceSlider { get; private set; }
    [field: SerializeField] public TextMeshProUGUI CurrentExperience { get; private set; }
    [field: SerializeField] public TextMeshProUGUI CurrentLevel { get; private set; }

    [field: SerializeField] private float AnimationDuration = 1f;


    public void Initialize(float health, float maxHealth, float exp, float expPerLevel, int level)
    {

        float healthFillAmount = (float) health / maxHealth;

        HealthOrbFill.fillAmount = healthFillAmount;
        CurrentHealth.text = ((int) health).ToString();

        UpdateHealth(health, maxHealth);

        float expFillAmount = exp / expPerLevel;

        CurrentExperience.text = $"{(int)(exp)}  ({((exp / expPerLevel) * 100):F2}%)";
        ExperienceSlider.value = expFillAmount;

        CurrentLevel.text = level.ToString();

    }

    public void UpdateHealth(float health, float maxHealth)
    {
        float targetHealthFillAmount = (float)health / maxHealth;

        LeanTween.value(HealthOrbFill.gameObject, HealthOrbFill.fillAmount, targetHealthFillAmount, AnimationDuration)
            .setOnUpdate((float value) =>
            {
                HealthOrbFill.fillAmount = value;
                CurrentHealth.text = ((int) health).ToString();
            })
            .setOnComplete(() =>
            {

                HealthOrbFill.fillAmount = targetHealthFillAmount;
                CurrentHealth.text = ((int) health).ToString();
            });
    }

    public void UpdateExperience(float exp, float expPerLevel, int level)
    {
        float targetExpFillAmount = exp / expPerLevel;

        ExperienceSlider.maxValue = 1f;

        LeanTween.value(ExperienceSlider.gameObject, ExperienceSlider.value, targetExpFillAmount, AnimationDuration)
            .setOnUpdate((float value) =>
            {
                ExperienceSlider.value = value;
                float currentExpPercentage = value * 100f;
                CurrentExperience.text = $"{(int)(value * expPerLevel)} ({currentExpPercentage:F2}%)";
            })
            .setOnComplete(() =>
            {
                CurrentLevel.text = level.ToString();
            });

    }







}
