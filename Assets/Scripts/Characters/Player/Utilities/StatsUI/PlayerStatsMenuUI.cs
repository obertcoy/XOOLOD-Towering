using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerStatsMenuUI
{

    [field: Header("Default Stats UI")]
    [field: SerializeField] public GameObject UI { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Level { get; private set; }
    [field: SerializeField] public TextMeshProUGUI MaxHealth { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Attack { get; private set; }

    [field: Header("Gold UI")]

    [field: SerializeField] public TextMeshProUGUI CurrentGold { get; private set; }

    private bool isOpen;

    public void Display(int level, float maxHealth, float attack, int gold)
    {
        if (!isOpen)
        {
            UpdateDefaultData(level, maxHealth, attack);

            UpdateGold(gold);

            UI.SetActive(true);

            isOpen = true;

            return;
        }

        Hide();
    }

    public void Hide()
    {
        UI.SetActive(false);

        isOpen = false;
    }


    private void UpdateDefaultData(int level, float maxHealth, float attack)
    {
        Level.text = level.ToString();
        MaxHealth.text = maxHealth.ToString("0.00");
        Attack.text = attack.ToString("0.00");
    }
    private void UpdateGold(int gold)
    {
        CurrentGold.text = gold.ToString();
    }

   






}
