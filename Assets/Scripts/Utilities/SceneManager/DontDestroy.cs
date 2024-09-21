using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private string objectID;

    private void Awake()
    {
        objectID = tag;
    }

    private void Start()
    {

        DontDestroy[] dontDestroyObjs = Object.FindObjectsOfType<DontDestroy>();

        foreach (var obj in dontDestroyObjs)
        {
            if (obj != this && obj.objectID == objectID) Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
