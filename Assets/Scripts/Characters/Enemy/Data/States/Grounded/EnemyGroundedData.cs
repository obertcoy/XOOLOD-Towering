using System;
using UnityEngine;

[Serializable]
public class EnemyGroundedData
{
    [field: SerializeField] [field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField] [field: Range(0f, 25f)] public float RotationSpeed { get; private set; } = 0.05f;

    [field: SerializeField] public EnemyWalkData WalkData { get; private set; }
    [field: SerializeField] public EnemyRunData RunData { get; private set; }

}
