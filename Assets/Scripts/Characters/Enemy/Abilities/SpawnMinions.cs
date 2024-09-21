using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnMinions : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> PrefabList { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] public float MinionsCount { get; private set; }

    private void Start()
    {
        Spawn();

        Destroy(gameObject, 3f);
    }

    private void Spawn()
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        for(int i = 0; i < MinionsCount; i++)
        {

            GameObject prefab = PrefabList[UnityEngine.Random.Range(0, PrefabList.Count - 1)];

            float angle = UnityEngine.Random.Range(0f, 360f);

            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * Radius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * Radius;

            Vector3 minionSpawnPosition = spawnPosition + new Vector3(x, 0f, z);

            GameObject gameObject = Instantiate(prefab, minionSpawnPosition, spawnRotation);
            gameObject.name = "Phoebus Devil's ( " + gameObject.name.Replace("(Clone)", "").Trim() + " )";

        }

    }


}
