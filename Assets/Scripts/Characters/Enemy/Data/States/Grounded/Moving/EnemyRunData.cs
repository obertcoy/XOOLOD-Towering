using System;
using UnityEngine;

[Serializable]
public class EnemyRunData
{
    [field: SerializeField] [field: Range(0f, 5f)] public float SpeedModifier { get; private set; } = 1f;

}
