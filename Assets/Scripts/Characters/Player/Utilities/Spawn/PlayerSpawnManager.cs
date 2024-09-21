using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerSpawnManager
{
    [field: SerializeField] public SpawnPositionSO PositionSO { get; set; }

    private static Action onSpawnCallback;

    public void SpawnAtMainScene(Player player)
    {

        onSpawnCallback = () =>
        {
            SpawnPosition spawnPosition = PositionSO.GetRespawnPosition(SceneEnum.MainScene);

            if (spawnPosition != null)
            {
                player.gameObject.transform.position = spawnPosition.Position;
                player.gameObject.transform.rotation = spawnPosition.Rotation;

                player.ResetPlayer();

                AreaText.Create("Teresa's Childhood Village");
            }
        };

        SceneLoader.Load(SceneEnum.MainScene);

    }

    public void SpawnAtBossScene(Player player)
    {

        onSpawnCallback = () =>
        {
            SpawnPosition spawnPosition = PositionSO.GetRespawnPosition(SceneEnum.BossScene);

            if (spawnPosition != null)
            {
                player.gameObject.transform.position = spawnPosition.Position;
                player.gameObject.transform.rotation = spawnPosition.Rotation;

                player.ResetPlayer();

                AreaText.Create("Phoebus's Chamber");
            }
        };

        SceneLoader.Load(SceneEnum.BossScene);

    }

    public void SpawnAtMazeScene(Player player)
    {

        onSpawnCallback = () =>
        {
            Transform spawnPosition = GameObject.Find("StartPoint").transform;

            if (spawnPosition != null)
            {
                player.gameObject.transform.position = spawnPosition.position + new Vector3(0, 0.5f, 0);

                player.ResetPlayer();

                AreaText.Create("Hendi the Rubick's Maze");
            }

        };

        SceneLoader.Load(SceneEnum.MazeScene);

    }

    public static void SpawnCallback()
    {
        if(onSpawnCallback != null)
        {
            onSpawnCallback();

            onSpawnCallback = null;
        }
    }



}
