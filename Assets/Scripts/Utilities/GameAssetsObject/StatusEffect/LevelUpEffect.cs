using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : GameAssetsObject
{
    public static LevelUpEffect Create(Vector3 position, GameObject parent)
    {

        GameObject gameObject = Instantiate(GameAssets.Instance.LevelUpEffect, position, Quaternion.identity, parent.transform);

        LevelUpEffect levelUpEffect = gameObject.GetComponent<LevelUpEffect>();

        return levelUpEffect;
    }

    #region GameAssets Methods

    protected override void LateUpdate()
    {
       

    }

    #endregion
}
