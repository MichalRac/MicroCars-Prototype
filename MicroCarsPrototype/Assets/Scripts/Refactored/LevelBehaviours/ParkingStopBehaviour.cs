using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingStopBehaviour : OnStopOnAreaBehaviour
{
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        CarController other = collision.GetComponent<CarController>();
        if (other == null)
        {
            Debug.Log("Something else stopped on parking spot");
            return;
        }
        else if (!other.States.IsMoving && other.States.IsTurn)
        {
            Debug.Log(collision.name + " stopped on " + gameObject.name);
        }

    }
}
