using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knot : MonoBehaviour
{
    public int knot_ID;
    public int xIndex;
    public int yIndex;
    public int parentKnotID; //the previous knot id
    public bool isWalkable = true;
    public bool isCheck = false;
    public GameObject ball;

    public void SetKnotIndex(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void SetBall(GameObject newball)
    {
        if(newball != null)
        {
            ball = newball;
            isWalkable = false;
        }
        else
        {
            ball = null;
            isWalkable = true;
        }
    }
    void OnMouseDown()
    {   
        BallSelectedController.instance.SelectKnot(this);
        //Debug.Log("x Index:" + xIndex + " y Index" + yIndex);
    }
}
