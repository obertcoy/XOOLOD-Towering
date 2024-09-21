using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LockSystemInfo : MonoBehaviour
{
   [field: SerializeField] public Slider HealthSlider { get; private set; }
   [field: SerializeField] public TextMeshProUGUI HealthText { get; private set; }
   [field: SerializeField] public TextMeshProUGUI EnemyInfo { get; private set; }

    public void Initialize(string name, float level, float health, float maxHealth)
    {
        SetEnemyInfo(name, level);
        SetHealthText(health, maxHealth);
        HealthSlider.maxValue = maxHealth;
    }

    public void SetEnemyInfo(string name, float level)
    {
        EnemyInfo.SetText("Lv. " + level + " " + name);
    }

    public void SetHealthText(float health, float maxHealth)
    {
        HealthText.SetText((int) health + " / " + (int) maxHealth);
        HealthSlider.value = health;
    }



}
