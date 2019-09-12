using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineBehaviour : OnTouchAreaBehaviour
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        CarController other = collision.GetComponent<CarController>();
        if (other == null)
        {
            Debug.Log("Something else passed finish line");
            return;
        }

        if (!other.States.IsLevelFinished)
        {

        }

    }

}
