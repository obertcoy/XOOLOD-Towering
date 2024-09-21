using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPosition
{
    public SceneEnum Scene;
    public Vector3 Position;
    public Quaternion Rotation;
}

[CreateAssetMenu(fileName = "SpawnPosition", menuName = "Custom/Characters/Spawn/Player")]
public class SpawnPositionSO : ScriptableObject
{
    

    [field: SerializeField] public List<SpawnPosition> SpawnPositions { get; private set; } = new List<SpawnPosition>();

    public SpawnPosition GetRespawnPosition(SceneEnum scene)
    {
        SpawnPosition matchingPosition = SpawnPositions.Find(position => position.Scene == scene);

        if (matchingPosition != null)
        {
            return matchingPosition;
        }

        return null;
    }

}
