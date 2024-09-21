using System;
using UnityEngine;

[Serializable]
public class EnemyActiveAbilityData
{
    [field: SerializeField] [field: Range(1f, 10f)] public float DamageMultiplier { get; set; }
    [field: SerializeField] [field: Range(1, 18)] public int HitCount { get; set; }

}
