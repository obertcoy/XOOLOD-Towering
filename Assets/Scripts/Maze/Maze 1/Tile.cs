using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int X { get; set; }
    public int Y { get; set; }
    public Tile Prev { get; set; }
    public float Heur { get; private set; }

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetHeur(int goalX, int goalY, float e)
    {
        int dx = X - goalX;
        int dy = Y - goalY;
        Heur = Mathf.Sqrt(dx * dx + dy * dy) - e;
    }
}