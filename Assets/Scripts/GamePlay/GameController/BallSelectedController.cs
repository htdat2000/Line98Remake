using UnityEngine;

public class BallSelectedController : MonoBehaviour
{
    public static BallSelectedController instance;
    Knot startKnot;
    Knot endKnot;
    GridManager gridManager;
    
    enum State
    {
        isSelectingBall,
        isFree
    }

    State state = State.isFree;
    public GameObject selectedBall;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than 1 BallSelectedController in scene");
            return;
        }
        instance = this;
    }

    void Start()
    {
        gridManager = GridManager.instance;
    }

    public void SelectKnot(Knot knot)
    {
        if(state != State.isFree)
        {
            return;
        }
        //GridManager.instance.SelectKnot(knot);
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
            if(CheckIsEndKnot(startKnot))
            {
                ResetKnot();
                return;
            }
            else 
            {
                if(gridManager.FindPath(startKnot, endKnot) && startKnot.ball != null)
                {   
                    SetState();
                    startKnot.SetBall(null);
                    endKnot.SetBall(selectedBall);
                    selectedBall.GetComponent<MovingAI>().Moving(gridManager.ReturnPathRoute(endKnot));
                    //Debug.Log("Has Path");
                }
                ResetKnot();    
            }
        }
        if(knot.ball != null)
        {
            selectedBall = knot.ball;
        }
    }

    bool CheckIsEndKnot(Knot knot)
    {
        if(knot.knot_ID == endKnot.knot_ID)
        {
            return true;
        }
        return false;
    }

    void ResetKnot()
    {
        startKnot = null;
        endKnot = null;
        foreach (Knot knot in gridManager.grid)
        {
            knot.isCheck = false;
        }
    }

    public void SetState()
    {
        if(state != State.isFree)
        {
            state = State.isFree;
            selectedBall = null;
            BallSpawner.instance.SpawnBall();
        }
        else
        {
            state = State.isSelectingBall;
        }
    }
}
