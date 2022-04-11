using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAI : MonoBehaviour
{
    float speed = 0.1f;
    Knot targetKnot;
    
    bool GetTargetKnot(Stack<Knot> pathRoute)
    {
        if(pathRoute.Count > 0)
        {
            targetKnot = pathRoute.Pop();
            return true;
        }
        else 
        {
            BallSelectedController.instance.SetState(); //END PATH
            return false;
        }
    }

    public void Moving(Stack<Knot> pathRoute)
    {
        Stack<Knot> path = pathRoute;
        while(true)
        {
            if(targetKnot == null)
            {
                if(!GetTargetKnot(path))
                {
                    return;
                }
            }
            else
            {
                Vector2 dir = targetKnot.transform.position - transform.position;
                gameObject.transform.Translate(dir.normalized * speed * Time.deltaTime);
                if(Vector2.Distance(gameObject.transform.position, targetKnot.transform.position) <= 0.1)
                {
                    if(!GetTargetKnot(path))
                    {
                        break;
                    }
                }
            }
        }
    }
}
