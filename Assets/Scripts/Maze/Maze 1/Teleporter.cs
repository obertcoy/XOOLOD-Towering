using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool isTeleportUp;

    private static Dictionary<Vector3, bool> teleportCooldowns = new Dictionary<Vector3, bool>();

    private bool isInTeleport = false;
    private bool teleportReady = false;

    void Start()
    {
        teleportCooldowns.Add(transform.position, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInTeleport && !teleportCooldowns[transform.position])
        {
            teleportReady = true;
        }
    }

    private void OnTriggerEnter(Collider other){
        isInTeleport = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isInTeleport = true;
        if (teleportReady && other.CompareTag("Player")){
            if (isTeleportUp) other.transform.position += new Vector3(0f, 5.1f, 0f);
            else other.transform.position -= new Vector3(0f, 5f, 0f);

            teleportReady = false;
            isInTeleport = false;
            teleportCooldowns[transform.position] = true;
            StartCoroutine(ResetCooldown(transform.position));
        }
    }

    private void OnTriggerExit(Collider other){
        isInTeleport = false;
    }

    IEnumerator ResetCooldown(Vector3 teleporterTransform)
    {
        yield return new WaitForSeconds(5f);
        isInTeleport = false;
        teleportCooldowns[teleporterTransform] = false;
    }
}
