using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;

    void Start()
    {
        SpawnBall(GridManager.instance.grid[2, 4]);
    }

    void SpawnBall(Knot knot)
    {
        GameObject newBall = Instantiate(ball, knot.gameObject.transform.position, Quaternion.identity);
        knot.ball = newBall;
        knot.isWalkable = false;
    }

}
