using System;
using UnityEngine;

[Serializable]
public class PlayerRuntimeStatsData {

    public float Health
    {
        get { return health; }
        set { health = Mathf.Max(value, 0); }
    }

    public float HealthRegenCooldown
    {
        get { return healthRegenCooldown; }
        set { healthRegenCooldown = Mathf.Max(value, 0); }
    }

    [field: SerializeField] [field: Range(1, 100)] public int Level { get; set; } = 1;

    [field: SerializeField] public float CurrentExp { get; set; } = 0f;

    [field: SerializeField] public float CurrentAttackDamage { get; set; }
    public int CurrentGold
    {
        get { return currentGold; }
        set { currentGold = Mathf.Max(value, 0); } 
    }

    [field: SerializeField] private float health;
    [field: SerializeField] private int currentGold;
    [field: SerializeField] private float healthRegenCooldown;

    public void Initialize(float health)
    {
        Health = health;
    }

    public void LevelUp(float expPerLevel)
    {
        Level += 1;
        CurrentExp -= expPerLevel;
    }


}
