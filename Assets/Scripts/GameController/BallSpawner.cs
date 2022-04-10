using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;

    void Start()
    {
        int count = 0;
        while(count < 5)
        {
            int xValue = Random.Range(0, 8);
            int yValue = Random.Range(0, 8);
            if(SpawnBall(GridManager.instance.grid[xValue, yValue]))
            {
                count++;
            }
        }
    }

    bool SpawnBall(Knot knot)
    {
        if(knot.isWalkable != false)
        {
            GameObject newBall = Instantiate(ball, knot.gameObject.transform.position, Quaternion.identity);
            knot.ball = newBall;
            knot.isWalkable = false;
            return true;
        }
        return false;
    }

}
