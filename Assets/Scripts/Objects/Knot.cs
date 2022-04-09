using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knot : MonoBehaviour
{
    public int xIndex;
    public int yIndex;
    public bool isWalkable = true;
    public bool isCheck = false;

    public void SetKnotIndex(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    void OnMouseDown()
    {   
        GridManager.instance.SelectKnot(this);
        //Debug.Log("x Index:" + xIndex + " y Index" + yIndex);
    }
}
