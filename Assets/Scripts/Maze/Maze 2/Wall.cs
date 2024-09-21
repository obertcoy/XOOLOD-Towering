using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnEnable()
    {
        MazeGenerator2.removeWall += destroy;
    }

    private void destroy(GameObject wall)
    {
        if (wall == this.gameObject) Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        MazeGenerator2.removeWall -= destroy;
    }
}
