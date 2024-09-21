using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : GameAssetsObject
{

    public static DeadEffect Create(Vector3 position, GameObject parent)
    {

        GameObject gameObject = Instantiate(GameAssets.Instance.DeadEffect, position, Quaternion.identity, parent.transform.parent);

        DeadEffect deadEffect = gameObject.GetComponent<DeadEffect>();

        return deadEffect;
    }

    #region GameAssets Methods

    protected override void LateUpdate()
    {

    }

    #endregion
}
