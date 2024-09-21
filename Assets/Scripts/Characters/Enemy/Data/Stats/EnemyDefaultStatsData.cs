using System;
using UnityEngine;

[Serializable]
public class EnemyDefaultStatsData
{
    [field: SerializeField] [field: Range(1, 100)] public int MinLevel { get; private set; }
    [field: SerializeField] [field: Range(1, 100)] public int MaxLevel { get; private set; }

    [field: SerializeField] [field: Range(1f, 100000f)] public float MinLevelMaxHealth { get; private set; }
    [field: SerializeField] [field: Range(1f, 10000f)] public float MinLevelAttackDamage { get; private set; }

    [field: SerializeField] [field: Range(1f, 10000f)] public float MinLevelExpDrop { get; private set; }
    [field: SerializeField] [field: Range(1, 10000)] public int MinLevelGoldDrop { get; private set; }

    public int Level { get; private set; }
    public float MaxHealth { get; private set; }
    public float AttackDamage { get; private set; }

    public float ExpDrop { get; private set; }
    public int GoldDrop { get; private set; }

    public void Initialize(float healthModifier, float attackModifier, float expModifier, float goldModifier)
    {
        Level = UnityEngine.Random.Range(MinLevel, MaxLevel);

        int distance = Level - MinLevel;

        MaxHealth = MinLevelMaxHealth + (MinLevelMaxHealth * healthModifier * distance);
        AttackDamage = MinLevelAttackDamage + (MinLevelAttackDamage * attackModifier * distance);
        ExpDrop = MinLevelExpDrop + (MinLevelExpDrop * expModifier * distance);
        GoldDrop = (int)(MinLevelGoldDrop + (MinLevelGoldDrop * goldModifier * distance));
    }

}
