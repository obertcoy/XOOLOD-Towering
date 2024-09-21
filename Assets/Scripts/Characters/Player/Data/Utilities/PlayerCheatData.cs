using System;
using UnityEngine;

[Serializable]
public class PlayerCheatData
{

    [field: SerializeField] public float SpeedModifier { get; set; } = 1f;
    [field: SerializeField] public float AttackModifier { get; set; } = 1f;
    [field: SerializeField] public float CooldownModifier { get; set; } = 1f;
    [field: SerializeField] public float TakeDamageModifier { get; set; } = 1f;


}
