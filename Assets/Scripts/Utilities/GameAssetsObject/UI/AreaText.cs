using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaText : GameAssetsObject
{
    [field: Header("Area Text UI")]
    [field: SerializeField] private TextMeshProUGUI TextMesh { get; set; }

    public static AreaText Create(string areaName)
    {
        GameObject gameObject = Instantiate(GameAssets.Instance.AreaText);

        AreaText areaText = gameObject.GetComponent<AreaText>();

        Debug.Log("area text: " + areaText.TextMesh);

        areaText.SetText(areaName);

        return areaText;
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
        TextMesh.SetText(text);
    }

    #endregion

}
