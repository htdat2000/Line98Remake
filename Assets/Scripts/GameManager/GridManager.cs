using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        KnotGenerator.instance.InstantiateKnot(gridRow, gridColumn);    
    }

    public void AddKnot(int row, int column, Knot knot)
    {
        grid[row, column] = knot;
        grid[row, column].SetKnotIndex(row, column);
    }

    public void FindPath(Knot knot)
    {
        knot.isCheck = true;
        Queue<Knot> q = new Queue<Knot>(); 
        q.Enqueue(knot);
        if(q != null)
        {
            Knot checkKnot = q.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                int newXIndex = checkKnot.xIndex + xDirection[i];
                int newYIndex = checkKnot.yIndex + yDirection[i];
                if(newXIndex >= 0 && newXIndex < gridRow && newYIndex >= 0 && newYIndex < gridColumn)
                {
                    if(!grid[newXIndex, newYIndex].isCheck && grid[newXIndex, newYIndex].isWalkable)
                    {
                        Debug.Log("x:" +newXIndex + " " + "y:" +newYIndex);
                        if(CheckEndKnot(grid[newXIndex, newYIndex]))
                        {
                            return;
                        }
                        q.Enqueue(grid[newXIndex, newYIndex]);
                        grid[newXIndex, newYIndex].isCheck = true;
                    }
                }
            }
        }
    }

    bool CheckEndKnot(Knot knot)
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
            FindPath(startKnot);
        }
    }



}
