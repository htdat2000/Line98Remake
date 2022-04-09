using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnotGenerator : MonoBehaviour
{
    [SerializeField] GameObject knot;
    [SerializeField] GameObject knotHolder;
    public static KnotGenerator instance;
    protected void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 KnotGenerator in scene");
            return;
        }
        instance = this;
    }

    public async void InstantiateKnot(int row, int column)
    {
        int knotCount = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Vector2 pos = new Vector2 (i, j);
                GameObject newKnot = Instantiate(knot, pos, Quaternion.identity);
                newKnot.transform.SetParent(knotHolder.transform);
                AddKnotToGrid(i, j, newKnot);
            }
        }
    }

    void AddKnotToGrid(int row, int column, GameObject knot)
    {     
        GridManager.instance.AddKnot(row, column, knot);              
    }
}
