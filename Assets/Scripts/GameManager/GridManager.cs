using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance; 
    [Header("Grid SetUp")]
    public GameObject[,] grid = new GameObject [9, 9];
    private int gridRow = 9;
    private int gridColumn = 9;
    private int numOfKnot = 81;

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

    public void AddKnot(int row, int column, GameObject knot)
    {
        grid[row, column] = knot;
        grid[row, column].GetComponent<Knot>().SetKnotIndex(row, column);
    }
}
