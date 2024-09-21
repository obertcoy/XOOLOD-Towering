using System;
using UnityEngine;

[Serializable]
public class PlayerLevelModiferData
{
    [field: SerializeField] public float HealthModifier { get; private set; } = 0.3f;
    [field: SerializeField] public float AttackModifier { get; private set; } = 0.3f;

}
