using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner instance;
    
    public GameObject[] waitingBall = new GameObject[3];

    public GameObject ball;
    
    public GameObject knotHolder;
    public List<Knot> notHasBallKnots = new List<Knot>();
    public List<Knot> hasBallKnots = new List<Knot>();

    void Start()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
        GetBallsAtBeginning();
        SpawnBall();
    }

    public void SpawnBall()
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
            for (int i = 0; i < 3; i++)
            {
                int randValue = Random.Range(0, notHasBallKnots.Count - 1);
                SpawnRandomPosition(notHasBallKnots[randValue]);
                SetWaitingBallPosition(i, notHasBallKnots[randValue]);
            }
        }
    }

    void SpawnRandomPosition(Knot knot)
    {
        ChangeBallToOtherList(notHasBallKnots, hasBallKnots, knot);   
        GameObject newBall = Instantiate(ball, knot.gameObject.transform.position, Quaternion.identity);
        knot.ball = newBall;
        knot.isWalkable = false;           
    }

    void GetBallsAtBeginning()
    {
        for (int i = 0; i < knotHolder.transform.childCount ; i++)
        {
            notHasBallKnots.Add(knotHolder.transform.GetChild(i).GetComponent<Knot>());
        }
    }

    void ChangeBallToOtherList(List<Knot> removeList, List<Knot> addList, Knot knot)
    {
        addList.Add(knot);
        removeList.Remove(knot);
    }

    void SetWaitingBallPosition(int waitingBallIndex,Knot knot)
    {
        waitingBall[waitingBallIndex].transform.position = knot.gameObject.transform.position; 
    }
}
