using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEntranceTeleporter : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.TryGetComponent<Player>(out Player player))
        {

            AreaWarning.Create(player, "Hendi the Rubick's Maze", 50, 60, () => player.SpawnManager.SpawnAtMazeScene(player));
        }
    }

}
