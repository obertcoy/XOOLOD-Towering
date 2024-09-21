using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsObject : MonoBehaviour
{
    [field: Header("Data")]

    [SerializeField] protected float DestroyTime = 2f;
    [SerializeField] protected Vector3 Offset = new Vector3(0, 1f, 0);
    [SerializeField] protected Vector3 RandomizeIntensity = new Vector3(0.15f, 0, 0.15f);

    protected Camera cameraMain;

    protected virtual void Awake()
    {
        cameraMain = Camera.main;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, DestroyTime);

        transform.localPosition += Offset;
        transform.localPosition += new Vector3
            (Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z)
            );
    }

    protected virtual void LateUpdate()
    {
        transform.LookAt(cameraMain.transform);
        transform.rotation = Quaternion.LookRotation(cameraMain.transform.forward);
    }
}
