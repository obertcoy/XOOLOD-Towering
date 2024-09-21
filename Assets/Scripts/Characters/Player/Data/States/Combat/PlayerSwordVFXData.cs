using System;
using UnityEngine;

[Serializable]
public class PlayerSwordVFXData
{

    public GameObject OutwardSlashVFX { get; private set; }
    public GameObject InwardSlashVFX { get; private set; }
    public GameObject ThrustSlashVFX { get; private set;}

    public void InitializeGameObject()
    {
        
        OutwardSlashVFX = GameObject.Find("OutwardSlash_VFX");
        InwardSlashVFX = GameObject.Find("InwardSlash_VFX");
        ThrustSlashVFX = GameObject.Find("ThrustSlash_VFX");


    }

}
