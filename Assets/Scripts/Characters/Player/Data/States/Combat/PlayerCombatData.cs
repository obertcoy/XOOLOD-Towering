using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCombatData 
{
    [field: SerializeField] public PlayerSwordData SwordData { get; private set; }
    [field: SerializeField] public PlayerBasicAttackData BasicAttackData { get; private set; }

}
