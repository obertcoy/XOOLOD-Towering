using System;
using UnityEngine;

[Serializable]
public class EnemyAttackData
{
    [field: SerializeField] [field: Range(1f, 10f)] public float DamageMultiplier { get; private set; }
    [field: SerializeField] [field: Range(1, 18)] public int HitCount { get; private set; }
    [field: SerializeField] [field: Range(0f, 50f)] public float AttackRange { get; private set; }
    [field: SerializeField] [field: Range(0f, 50f)] public float AttackCooldown { get; private set; }
}
