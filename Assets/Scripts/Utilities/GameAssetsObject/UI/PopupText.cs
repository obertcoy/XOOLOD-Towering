using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PopupTextType
{
    Damage,
    LevelUp,
    Drop
}

public class PopupText : GameAssetsObject
{

    [field: Header("Popup Text Data")]

    [field: SerializeField] private Vector3 MoveOffset { get; set; }

    private TextMeshPro textMesh;

    public static PopupText Create(Vector3 position, GameObject parent, string text, PopupTextType type, Color color = default)
    {

        GameObject gameObject = null;

        switch (type)
        {
            case PopupTextType.Damage:
                gameObject = Instantiate(GameAssets.Instance.DamagePopupText, position, Quaternion.identity, parent.transform);
                break;
            case PopupTextType.LevelUp:
                gameObject = Instantiate(GameAssets.Instance.LevelUpPopupText, position, Quaternion.identity, parent.transform);
                break;
            case PopupTextType.Drop:
                gameObject = Instantiate(GameAssets.Instance.DropPopupText, position, Quaternion.identity, parent.transform);
                break;
        }

        if (gameObject == null) return null;

        PopupText popupText = gameObject.GetComponent<PopupText>();

        popupText.SetText(text);

        if (color != default) popupText.SetColor(color);

        return popupText;
    }

    #region GameAssets Methods

    protected override void Awake()
    {
        base.Awake();

        textMesh = gameObject.GetComponent<TextMeshPro>();

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        transform.position += MoveOffset * Time.deltaTime;
    }

    #endregion

    #region Main Methods


    private void SetText(string text)
    {
        textMesh.SetText(text);
    }

    private void SetColor(Color color)
    {
        textMesh.color = color;
    }

    #endregion
}
