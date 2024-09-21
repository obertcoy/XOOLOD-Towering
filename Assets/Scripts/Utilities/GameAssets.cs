using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets Instance
    {
        get
        {
            if (_instance == null) _instance = Instantiate(Resources.Load("GameAssets") as GameObject).GetComponent<GameAssets>();

            return _instance;
        }
    }

    [field: Header("Weapon")]
    [field: SerializeField] public GameObject Sword { get; private set; }

    [field: Header("Utilities")]

    [field: SerializeField] public GameObject HitEffect { get; private set; }
    [field: SerializeField] public GameObject LevelUpEffect { get; private set; }
    [field: SerializeField] public GameObject DeadEffect { get; private set; }
    [field: SerializeField] public GameObject LockCrosshair { get; private set; }
    [field: SerializeField] public GameObject DamagePopupText { get; private set; }
    [field: SerializeField] public GameObject LevelUpPopupText { get; private set; }
    [field: SerializeField] public GameObject DropPopupText { get; private set; }
    [field: SerializeField] public GameObject AreaWarning { get; private set; }
    [field: SerializeField] public GameObject AreaText { get; private set; }
    [field: SerializeField] public GameObject WinCanvas { get; private set; }

    [field: SerializeField] public GameObject CheatPopup { get; private set; }

    [field: Header("Ability Rebind")]
    [field: SerializeField] public GameObject AbilityRebindUI { get; private set; }


    [field: Header("NPC Shop")]
    [field: SerializeField] public GameObject AbilityShopUI { get; private set; }
    [field: SerializeField] public GameObject ChangeAbilityShopUI { get; private set; }

    [field: Header("NPC Shop Utility")]
    [field: SerializeField] public Sprite GoldSprite { get; private set; }


}
