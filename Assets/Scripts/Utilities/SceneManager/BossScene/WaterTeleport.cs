using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTeleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.transform.position = new Vector3(148.87f, 28f, 64.21f);
            player.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
