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

    public void InstantiateKnot(int column, int row)
    {
        int knotCount = 0;
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Vector2 pos = new Vector2 (i, j);
                GameObject newKnot = Instantiate(knot, pos, Quaternion.identity);
                newKnot.transform.SetParent(knotHolder.transform);
                AddKnotToGrid(i, j, newKnot, knotCount);
                knotCount ++;
            }
        }
    }

    void AddKnotToGrid(int column, int row, GameObject knot, int _knot_ID)
    {     
        GridManager.instance.AddKnot(column, row, knot.GetComponent<Knot>(), _knot_ID);              
    }
}
