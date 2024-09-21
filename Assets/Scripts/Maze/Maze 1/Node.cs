using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Tile Src { get; set; }
    public Tile Dest { get; set; }
    public float Price { get; private set; }

    public Node(Tile src, Tile dest)
    {
        Src = src;
        Dest = dest;
        CalculatePrice();
    }

    private void CalculatePrice()
    {
        int x = Src.X - Dest.X;
        int y = Src.Y - Dest.Y;
        Price = Mathf.Sqrt(x * x + y * y);
    }
}
