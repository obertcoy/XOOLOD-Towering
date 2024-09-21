using System;
using UnityEngine;

[Serializable]
public class EnemyLevelModifierData
{
    [field: SerializeField] [field: Range(0f, 1f)] public float AttackModifier { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float HealthModifier { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float ExpDropModifier { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float GoldDropModifier { get; private set; }

}
