using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Custom/Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public EnemyNameEnum Name { get; private set; }
    [field: SerializeField] public EnemyGroundedData GroundedData { get; private set; }
    [field: SerializeField] public EnemyAirborneData AirborneData { get; private set; }
    [field: SerializeField] public EnemyCombatData CombatData { get; private set; }
    
}
