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
        InstantiateKnot(9,9);
    }

    public async void InstantiateKnot(int row, int column)
    {
        DebugMissingPrefab();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Vector2 pos = new Vector2 (i, j);
                GameObject newKnot = Instantiate(knot, pos, Quaternion.identity);
                newKnot.transform.SetParent(knotHolder.transform);
            }
        }
    }

    void DebugMissingPrefab()
    {
        if(knot == null)
        {
            Debug.LogError("No knot prefab");
        }
        if(knotHolder == null)
        {
            Debug.LogError("No knot holder");
        }
    }
}
