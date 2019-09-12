using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPhysicsBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CarController other = collision.gameObject.GetComponent<CarController>();
        if(other == null)
        {
            Debug.Log(other.gameObject.name + " has hit a wall");
        }
        else
        {
            other.States.HitWall = true;
        }

    }
}
