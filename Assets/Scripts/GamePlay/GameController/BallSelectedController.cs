using UnityEngine;

public class BallSelectedController : MonoBehaviour
{
    public static BallSelectedController instance;
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

    public void SelectKnot(Knot knot)
    {
        if(state != State.isFree)
        {
            return;
        }
        GridManager.instance.SelectKnot(knot);
        selectedBall = knot.ball;
    }

    public void SetState()
    {
        if(state != State.isFree)
        {
            state = State.isFree;
            selectedBall = null;
        }
        else
        {
            state = State.isSelectingBall;
        }
    }
}
