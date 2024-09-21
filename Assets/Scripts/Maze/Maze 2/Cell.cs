using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool visited { get; private set; } = false;

    public void visit()
    {
        visited = true;
        GetComponent<BoxCollider>().enabled = false;
    }

}
