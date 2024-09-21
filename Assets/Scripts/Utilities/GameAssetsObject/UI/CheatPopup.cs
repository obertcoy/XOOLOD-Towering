using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheatPopup : GameAssetsObject
{
    [field: Header("Cheat Popup UI")]
    [field: SerializeField] private TextMeshProUGUI CheatCode { get; set; }

    public static CheatPopup Create(string cheatcode)
    {
        GameObject gameObject = Instantiate(GameAssets.Instance.CheatPopup);

        CheatPopup cheatPopup = gameObject.GetComponent<CheatPopup>();

        cheatPopup.SetText(cheatcode);

        return cheatPopup;
    }

    #region GameAssetsObject Methods

    protected override void Awake()
    {

    }

    protected override void LateUpdate()
    {

    }

    #endregion

    #region Main Methods

    private void SetText(string text)
    {
        CheatCode.SetText(text);
    }

    #endregion

}
