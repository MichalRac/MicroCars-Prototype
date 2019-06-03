using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPhysicsBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CarStates other = collision.gameObject.GetComponent<CarStates>();
        if(other == null)
        {
            Debug.Log(other.gameObject.name + " has hit a wall");
        }
        else
        {
            other.HitWall = true;
        }

    }
}
