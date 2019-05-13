using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsTurning : CarPhysicsRoot
{
    [SerializeField]
    private float turnPower = 5.0f;
    [SerializeField]
    private float turnSwipeResponsivness = 1.0f;


    public void SwipeAction(string direction, float swipeLenght)
    {
        if (stateIsMoving == false)
            return;

        if (direction == "left")
        {
            rb2D.AddTorque(turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine(carMovement.MaintainVelocityRotation());
        }

        else if (direction == "right")
        {
            rb2D.AddTorque(-turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine(carMovement.MaintainVelocityRotation());
        }

    }

    public void TriggerCustomTurnEffect(float turnDurationSec, float targetAngle)
    {
        StartCoroutine(CustomTurnEffect(turnDurationSec, targetAngle));
    }

    public IEnumerator CustomTurnEffect(float turnDurationSec, float targetAngle)
    {
        Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, targetAngle);
        float angleDifference = Quaternion.Angle(gameObject.transform.rotation, targetRotation);

        float timeElapsed = 0.0f;

        while (Quaternion.Angle(gameObject.transform.rotation, targetRotation) > 0.001f)
        {

            Debug.Log(timeElapsed);
            timeElapsed += Time.fixedDeltaTime;

            float deltaAngle = angleDifference * (Time.fixedDeltaTime / turnDurationSec);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, deltaAngle);
            yield return new WaitForFixedUpdate();

        }
    }

}
