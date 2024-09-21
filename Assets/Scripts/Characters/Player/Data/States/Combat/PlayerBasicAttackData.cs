using System;
using UnityEngine;

[Serializable]
public class PlayerBasicAttackData
{
    [field: SerializeField] [field: Range(1, 5)] public int BasicAttackComboLimit { get; private set; } = 3;
    [field: SerializeField] [field: Range(0f, 1f)] public float BasicAttackComboInterval { get; private set; } = 0.5f;
}



