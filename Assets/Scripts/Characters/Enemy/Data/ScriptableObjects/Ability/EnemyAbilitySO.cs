using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAbilitiesNameEnum
{
    SpawnMinions,
    DragonBreath
}

public class EnemyAbilitySO : AbilitySO
{
    [field: SerializeField] public EnemyAbilitiesNameEnum Name { get; private set; }

}
