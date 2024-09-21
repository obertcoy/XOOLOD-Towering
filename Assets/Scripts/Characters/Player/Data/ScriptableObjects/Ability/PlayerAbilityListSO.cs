using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableList", menuName = "Custom/Characters/Ability/Player/Available List")]
public class PlayerAbilityListSO : ScriptableObject
{

    [field: Header("Active Abilities")]
    [field: SerializeField] public List<PlayerActiveAbilitySO> ActiveAbilityList { get; private set; }

    [field: Header("Passive Abilities")]
    [field: SerializeField] public List<PlayerPassiveAbilitySO> PassiveAbilityList { get; private set; }

    public PlayerAbilitySO GetAbility(PlayerAbilitiesNameEnum name)
    {
        foreach(PlayerAbilitySO ability in ActiveAbilityList)
        {
            if (ability.Name.Equals(name)) return ability;
        }

        foreach (PlayerAbilitySO ability in PassiveAbilityList)
        {
            if (ability.Name.Equals(name)) return ability;
        }

        return null;
    }
}
