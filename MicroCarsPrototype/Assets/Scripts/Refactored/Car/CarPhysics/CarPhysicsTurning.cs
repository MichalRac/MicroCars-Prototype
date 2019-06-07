using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsTurning : MonoBehaviour
{
    [SerializeField]
    private float turnPower = 5.0f;
    [SerializeField]
    private float turnSwipeResponsivness = 1.0f;

    private CarStates states;
    private CarPhysicsMovement movement;
    private Rigidbody2D rb2D;


    private void Awake()
    {
        states = GetComponent<CarStates>();
        movement = GetComponent<CarPhysicsMovement>();
        rb2D = GetComponent<Rigidbody2D>();
        Debug.Assert(states, $"{typeof(CarStates)} is null");
        Debug.Assert(movement, $"{typeof(CarPhysicsMovement)} is null");
        Debug.Assert(rb2D, $"{typeof(Rigidbody2D)} is null");
    }


    public void SwipeAction(string direction, float swipeLenght, Vector3 swipeVector)
    {
        Debug.Log(states.IsMoving);
        if (states.IsMoving == false)
            return;

        
        if (direction == "left")
        {
            rb2D.AddTorque(turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine(movement.MaintainVelocityRotation());
        }
        else if (direction == "right")
        {
            rb2D.AddTorque(-turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine(movement.MaintainVelocityRotation());
        }
        else if (direction == "any")
        {
            
            if(Vector2.SignedAngle(swipeVector.normalized, rb2D.velocity.normalized) > 0)
            {
                rb2D.AddTorque(turnPower * (swipeLenght * turnSwipeResponsivness));
                StartCoroutine(movement.MaintainVelocityRotation());
            }
            else if(Vector2.SignedAngle(swipeVector.normalized, rb2D.velocity.normalized) < 0)
            {
                rb2D.AddTorque(-turnPower * (swipeLenght * turnSwipeResponsivness));
                StartCoroutine(movement.MaintainVelocityRotation());
            }
        }
        else
        {
            Debug.Log($"Unknown direction passed: {direction}");
        }
    }

    public void TurnOnCustomTurnEffect(float turnDurationSec, float targetAngle)
    {
        StartCoroutine(CustomTurnEffect(turnDurationSec, targetAngle));
    }

    public void TurnOffCustomTurnEffect()
    {
        this.StopAllCoroutines();
    }

    public IEnumerator CustomTurnEffect(float turnDurationSec, float targetAngle)
    {
        Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, targetAngle);
        float angleDifference = Quaternion.Angle(gameObject.transform.rotation, targetRotation);


        while (Quaternion.Angle(gameObject.transform.rotation, targetRotation) > 0.001f)
        {

            float deltaAngle = angleDifference * (Time.deltaTime / turnDurationSec);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, deltaAngle);
            yield return new WaitForFixedUpdate();

        }
    }

}
