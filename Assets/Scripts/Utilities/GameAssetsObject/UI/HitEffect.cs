using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : GameAssetsObject
{


    public static HitEffect Create(Vector3 position, GameObject parent)
    {

        GameObject gameObject = Instantiate(GameAssets.Instance.HitEffect, position, Quaternion.identity, parent.transform);

        HitEffect hitEffect = gameObject.GetComponent<HitEffect>();

        return hitEffect;
    }


}
