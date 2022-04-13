using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballHolder;
    public List<Knot> notHasBallKnots = new List<Knot>();
    public List<Knot> hasBallKnots = new List<Knot>();

    void Start()
    {
        GetBallsAtBeginning();
        SpawnBall();
        Debug.Log(notHasBallKnots[1].knot_ID);
    }

    void SpawnBall()
    {
        if(notHasBallKnots.Count < 3)
        {
            for (int i = 0; i < notHasBallKnots.Count; i++)
            {
                GameObject newBall = Instantiate(ball, notHasBallKnots[i].gameObject.transform.position, Quaternion.identity);
                notHasBallKnots[i].ball = newBall;
                notHasBallKnots[i].isWalkable = false;
            }     
        }
        else
        {
            int count = 0;
            while(count < 3)
            {
                int randValue = Random.Range(0, notHasBallKnots.Count);
                SpawnRandomPosition(notHasBallKnots[randValue]);
                count++; 
            }
        }
    }

    void SpawnRandomPosition(Knot knot)
    {
        GameObject newBall = Instantiate(ball, knot.gameObject.transform.position, Quaternion.identity);
        knot.ball = newBall;
        knot.isWalkable = false;
        ChangeBallToOtherList(notHasBallKnots, hasBallKnots, knot);          
    }

    void GetBallsAtBeginning()
    {
        for (int i = 0; i < ballHolder.transform.childCount ; i++)
        {
            notHasBallKnots.Add(ballHolder.transform.GetChild(i).GetComponent<Knot>());
        }
    }

    void ChangeBallToOtherList(List<Knot> removeList, List<Knot> addList, Knot knot)
    {
        addList.Add(knot);
        removeList.Remove(knot);
    }



}
