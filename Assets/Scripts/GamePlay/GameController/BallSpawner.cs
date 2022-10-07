using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner instance;
    
    //public GameObject[] waitingBall = new GameObject[3];

    [SerializeField] GameObject ball;
    List<GameObject> balls = new List<GameObject>();
    [SerializeField] GameObject ballHolder;

    [SerializeField] GameObject knotHolder;
    List<Knot> notHasBallKnots = new List<Knot>();
    List<Knot> hasBallKnots = new List<Knot>();

    void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
        for (int i = 0; i < 81; i++)
        {
            GameObject newBall = Instantiate(ball, transform.position, Quaternion.identity, ballHolder.transform);
            newBall.SetActive(false);
        }
    }
    void Start()
    {
        GetKnotsAtBeginning();
    }


    #region Spawn Process
    void RandomKnot()
    {
        
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
            }
        }
    }

    void SpawnRandomPosition(Knot knot)
    {
        ChangeKnotToOtherList(notHasBallKnots, hasBallKnots, knot);   
        GameObject newBall = Instantiate(ball, knot.gameObject.transform.position, Quaternion.identity);
        knot.ball = newBall;
        knot.isWalkable = false;           
    } 

    void ChangeKnotToOtherList(List<Knot> removeList, List<Knot> addList, Knot knot)
    {
        removeList.Remove(knot);
        addList.Add(knot);
    }
    #endregion

    void GetKnotsAtBeginning()
    {
        for (int i = 0; i < knotHolder.transform.childCount ; i++)
        {
            notHasBallKnots.Add(knotHolder.transform.GetChild(i).GetComponent<Knot>());
        }
        //SpawnBall();
    }

    


}
