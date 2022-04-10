using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAI : MonoBehaviour
{
    int speed = 3;
    Knot targetKnot;
    public Stack<Knot> pathRoute = new Stack<Knot>();
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(targetKnot == null)
        {
            if(!GetTargetKnot())
            {
                return;
            }
        }
        else
        {
            Vector2 dir = targetKnot.transform.position - transform.position;
            gameObject.transform.Translate(dir * speed * Time.deltaTime);
            if(Vector2.Distance(gameObject.transform.position, targetKnot.transform.position) <= 0.1)
            {
                GetTargetKnot();
            }
        }
    }

    bool GetTargetKnot()
    {
        if(pathRoute.Count > 0)
        {
            targetKnot = pathRoute.Pop();
            return true;
        }
        else 
        {
            return false;
        }
    }
}
