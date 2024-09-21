using System;
using UnityEngine;

[Serializable]
public class EnemyRuntimeStatsData
{
    private float health;
    public float Health
    {
        get { return health; }
        set { health = Mathf.Max(value, 0f); }
    }
    public float CurrentAttackDamage { get; set; }

    public void Initialize(float health, float attackDamage)
    {
        Health = health;
        CurrentAttackDamage = attackDamage;
    }
}
