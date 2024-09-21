using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class EnemyCombatData
{
    [field: SerializeField] [field: Range(1f, 50f)] public float AggroRange { get; private set; } = 7.5f;
    [field: SerializeField] [field: Range(1f, 50f)] public float FlyingAggroRange { get; private set; } = 25f;
    [field: SerializeField] [field: Range(0f, 5f)] public float ChaseCooldown { get; private set; } = 0.5f;
    [field: SerializeField] [field: Range(0f, 5f)] public float AttackIntervalCooldown { get; private set; } = 2.5f;
    [field: SerializeField] [field: Range(0f, 5f)] public float TakeDamageAnimationCooldown { get; private set; } = 0.5f;

    [field: SerializeField] public List<EnemyAttackData> AttackData { get; private set; }

    
}
