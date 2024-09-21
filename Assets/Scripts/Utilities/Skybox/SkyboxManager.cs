using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public float SkySpeed = 0.5f;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", -Time.time * SkySpeed);
    }

}
