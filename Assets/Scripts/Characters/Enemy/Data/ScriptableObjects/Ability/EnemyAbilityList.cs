using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableList", menuName = "Custom/Characters/Ability/Enemy/Available List")]
public class EnemyAbilityList : ScriptableObject
{
    [field: Header("Active Abilities")]
    [field: SerializeField] public List<EnemyActiveAbilitySO> ActiveAbilityList { get; private set; }

    public EnemyAbilitySO GetAbility(EnemyAbilitiesNameEnum name)
    {
        foreach (EnemyAbilitySO ability in ActiveAbilityList)
        {
            if (ability.Name.Equals(name)) return ability;
        }

        return null;
    }
}
