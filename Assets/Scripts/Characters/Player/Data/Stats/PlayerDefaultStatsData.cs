using System;
using UnityEngine;

[Serializable]
public class PlayerDefaultStatsData
{

    [field: SerializeField] [field: Range(100f, 10000f)] public float MaxHealth { get; private set; } = 250f;
    [field: SerializeField] [field: Range(1, 100)] public int MaxLevel { get; private set; } = 100;
    [field: SerializeField] public float ExpPerLevel { get; private set; }
    [field: SerializeField] [field: Range(0f, 2f)] public float ConstantExpIncreaseModifier { get; private set; }
    [field: SerializeField] [field: Range(1f, 1000f)] public float ConstantExpPerLevel { get; private set; } = 500f;
    [field: SerializeField] [field: Range(0f, 1000f)] public float AttackDamage { get; set; } = 50f;

    [field: SerializeField] [field: Range(0f, 100f)] public float HealthRegenCooldown { get; set; } = 3f;

    public void LevelUp(float healthModifier, float attackModifier)
    {
        CalculateExpPerLevel();

        AttackDamage *= (1 + attackModifier);
        MaxHealth *= (1 + healthModifier);

    }

    private void CalculateExpPerLevel()
    {
        ExpPerLevel = ExpPerLevel + (ConstantExpPerLevel * ConstantExpIncreaseModifier);
    }



}
