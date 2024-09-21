using System;
using UnityEngine;

[Serializable]
public class EnemyStats 
{
    [field: Header("Stats")]
    [field: SerializeField] public EnemyDefaultStatsData DefaultData { get; private set; } 
    [field: SerializeField] public EnemyRuntimeStatsData RuntimeData { get; set; }
    [field: SerializeField] public EnemyLevelModifierData LevelModifierData { get; private set; }

    public void Initialize()
    {
        DefaultData.Initialize(
            LevelModifierData.HealthModifier,
            LevelModifierData.AttackModifier,
            LevelModifierData.ExpDropModifier,
            LevelModifierData.GoldDropModifier);

        RuntimeData.Initialize(DefaultData.MaxHealth, DefaultData.AttackDamage);
    }

}
