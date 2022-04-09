using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knot : MonoBehaviour
{
    private int xIndex;
    private int yIndex;
    private bool isWalkable = true;

    public void SetKnotIndex(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }
}
