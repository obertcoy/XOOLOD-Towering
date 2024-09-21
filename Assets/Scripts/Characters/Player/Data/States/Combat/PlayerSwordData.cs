using System;
using UnityEngine;

[Serializable]
public class PlayerSwordData
{
    public GameObject SwordHolder { get; private set; }
    public GameObject SheathHolder { get; private set; }
    
    public void InitializeGameObject()
    {
        SwordHolder = GameObject.FindWithTag("WeaponHolder");
        SheathHolder = GameObject.FindWithTag("SheatHolder");

    }

}
