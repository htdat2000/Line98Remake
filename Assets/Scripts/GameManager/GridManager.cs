using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    public static GridManager instance; 
    [Header("Grid SetUp")]
    [SerializeField] KnotGenerator knotGenerator;
    public Knot[,] grid = new Knot [9, 9];
    private int gridRow = 9;
    private int gridColumn = 9;
    //private int numOfKnot = 81;

    [Header("Path Finding Setup")]
    private Knot startKnot;
    private Knot endKnot;
    private Knot[] knots = new Knot [81];
    private int[] xDirection = {-1, 0, 1, 0};
    private int[] yDirection = {0, 1, 0, -1};
    

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than 1 GridManager in scene");
            return;
        }
        instance = this;
        knotGenerator.InstantiateKnot(gridColumn, gridRow);    
    }

    public void AddKnot(int column, int row, Knot knot, int _knot_ID)
    {
        grid[column, row] = knot;
        grid[column, row].SetKnotIndex(column, row);
        grid[column, row].knot_ID = _knot_ID;
        knots[_knot_ID] = knot;
    }

    public bool FindPath(Knot knot)
    {
        if(CheckIsEndKnot(knot))
        {
            return true;
        }
        knot.isCheck = true;
        knot.parentKnotID = 0;
        Queue<Knot> q = new Queue<Knot>();
        q.Enqueue(knot);
        while(q != null)
        {
            Knot checkKnot = q.Dequeue();  
            for (int i = 0; i < 4; i++)
            {
                int newXIndex = checkKnot.xIndex + xDirection[i];
                int newYIndex = checkKnot.yIndex + yDirection[i];
                if(newXIndex >= 0 && newXIndex < gridColumn && newYIndex >= 0 && newYIndex < gridRow)
                {
                    //Debug.Log("new X:" + newXIndex + "new Y:" +newYIndex);
                    //Debug.Log(checkKnot.knot_ID);
                    Knot nextKnot = grid[newXIndex, newYIndex];
                    if(nextKnot.isCheck == false && nextKnot.isWalkable == true)
                    {
                        nextKnot.parentKnotID = checkKnot.knot_ID;
                        //Debug.Log("x:" +newXIndex + " " + "y:" +newYIndex);
                        if(CheckIsEndKnot(nextKnot))
                        {
                            return true;
                        }
                        q.Enqueue(nextKnot);
                        nextKnot.isCheck = true;
                    }
                }
            }
        }
        return false;
    }

    bool CheckIsEndKnot(Knot knot)
    {
        if(knot.knot_ID == endKnot.knot_ID)
        {
            return true;
        }
        return false;
    }

    void ResetKnot()
    {
        startKnot = null;
        endKnot = null;
        foreach (Knot knot in grid)
        {
            knot.isCheck = false;
        }
    }

    public void SelectKnot(Knot knot)
    {
        if(startKnot == null)
        {
            startKnot = knot;
        }
        else
        {
            endKnot = knot;
        }
        if(startKnot != null && endKnot != null)
        {
            if(startKnot.knot_ID == endKnot.knot_ID)
            {
                return;
            }
            else 
            {
                if(FindPath(startKnot))
                {
                    BallSelectedController.instance.selectedBall.GetComponent<MovingAI>().pathRoute = ReturnPathRoute(endKnot);
                    //Debug.Log("Has Path");
                }    
            }
        }
    }

    Stack<Knot> ReturnPathRoute(Knot knot)
    {
        Stack<Knot> pathRoute = new Stack<Knot>();
        pathRoute.Push(knot);
        int parentID = knot.parentKnotID;
        while(parentID != 0)
        {
            Knot nextKnot = Array.Find<Knot>(knots, _knot => _knot.knot_ID == pathRoute.Peek().parentKnotID);
            pathRoute.Push(nextKnot);
            parentID = nextKnot.parentKnotID;
        }
        ResetKnot();
        return pathRoute;
    }
}
