using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsTurning : CarPhysicsRoot
{
    protected float turnPower = 5.0f;
    protected float turnSwipeResponsivness = 1.0f;


    public void SwipeAction(string direction, float swipeLenght)
    {
        if (stateIsMoving == false)
            return;

        if (direction == "left")
        {
            rb2D.AddTorque(turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine(carMovement.maintainVelocityRotation());
        }

        else if (direction == "right")
        {
            rb2D.AddTorque(-turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine(carMovement.maintainVelocityRotation());
        }
    }

}
