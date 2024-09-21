using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour
{
    [SerializeField] private string AreaName;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Player>() != null)
        {
            AreaText.Create(AreaName);
        }
    }

   
}
