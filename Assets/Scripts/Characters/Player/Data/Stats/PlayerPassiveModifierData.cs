using System;
using UnityEngine;


[Serializable]
public class PlayerPassiveModifierData
{
    [field: SerializeField] [field: Range(0f, 10f)] public float ExpModifier { get; set; } = 1f;
    [field: SerializeField] [field: Range(0f, 100f)] public float HealthRegenModifier { get;  set; } = 1f;
    [field: SerializeField] [field: Range(1f, 5f)] public float AttackSpeed { get; set; } = 1f;


}
