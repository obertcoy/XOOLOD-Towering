using System;
using UnityEngine;

[Serializable]
public class PlayerActiveAbilityData
{

    [field: SerializeField] [field: Range(1f, 10f)] public float DamageMultiplier { get; set; }
    [field: SerializeField] [field: Range(1, 18)] public int HitCount { get; set; }
    [field: SerializeField] [field: Range(1, 20)] public int Cooldown { get; set; }
    [field: SerializeField] public float CurrentCooldown { get; set; } = 0;

}
