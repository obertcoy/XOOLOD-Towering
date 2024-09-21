using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInfo : MonoBehaviour
{
    [field: SerializeField] private Transform Canvas;

    private Camera cameraMain;

    private void Start()
    {
        cameraMain = Camera.main;
    }

    private void Update()
    {
        Canvas.LookAt(cameraMain.transform);
        Canvas.rotation = Quaternion.LookRotation(cameraMain.transform.forward);
    }
}
