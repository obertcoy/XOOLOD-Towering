using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeExitTeleporter : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Player>(out Player player))
        {

            AreaWarning.Create(player, "Teresa's Childhood Village", 1, 10, () => player.SpawnManager.SpawnAtMainScene(player));
        }
    }

}

