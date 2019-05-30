using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineBehaviour : OnTouchAreaBehaviour
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GameObject other = collision.gameObject;

        if (other.GetComponent<CarController>())
        {

        }

    }

}
