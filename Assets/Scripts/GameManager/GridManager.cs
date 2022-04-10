using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance; 
    [Header("Grid SetUp")]
    public Knot[,] grid = new Knot [9, 9];
    private int gridRow = 9;
    private int gridColumn = 9;
    private int numOfKnot = 81;

    [Header("Path Finding Setup")]
    private Knot startKnot;
    private Knot endKnot;
    private int[] xDirection = {0, 1, 0, -1};
    private int[] yDirection = {-1, 0, 1, 0};

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than 1 GridManager in scene");
            return;
        }
        instance = this;
    }
    void Start()
    {
        KnotGenerator.instance.InstantiateKnot(gridColumn, gridRow);    
    }

    public void AddKnot(int column, int row, Knot knot, int _knot_ID)
    {
        grid[column, row] = knot;
        grid[column, row].SetKnotIndex(row, column);
        grid[column, row].knot_ID = _knot_ID;
    }

    public bool FindPath(Knot knot)
    {
        if(CheckIsEndKnot(knot))
        {
            return true;
        }
        knot.isCheck = true;
        Queue<Knot> q = new Queue<Knot>();
        q.Enqueue(knot);
        while(q != null)
        {
            Knot checkKnot = q.Dequeue();  
            for (int i = 0; i < 4; i++)
            {
                int newXIndex = checkKnot.xIndex + xDirection[i];
                int newYIndex = checkKnot.yIndex + yDirection[i];
                if(newXIndex >= 0 && newXIndex < gridRow && newYIndex >= 0 && newYIndex < gridColumn)
                {
                    if(grid[newXIndex, newYIndex].isCheck == false && grid[newXIndex, newYIndex].isWalkable == true)
                    {
                        Debug.Log("x:" +newXIndex + " " + "y:" +newYIndex);
                        if(CheckIsEndKnot(grid[newXIndex, newYIndex]))
                        {
                            return true;
                        }
                        q.Enqueue(grid[newXIndex, newYIndex]);
                        grid[newXIndex, newYIndex].isCheck = true;
                    }
                }
            }
        }
        return false;
    }

    bool CheckIsEndKnot(Knot knot)
    {
        if(knot.xIndex == endKnot.xIndex && knot.yIndex == endKnot.yIndex)
        {
            ResetKnot();
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
                    Debug.Log("Has Path");
                }    
            }
        }
    }
}
