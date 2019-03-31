﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour {

    private const float minMovingSpeed = 0.001f;

    private bool isMoving = false;
    public float carSpeed = 0.0f;
    public float turnPower = 5.0f;

    public float deltaDragPower = 0.5f;
    public float deltaDragTime = 0.5f;
    public float firstSlowTime = 0.5f;

    private Rigidbody2D rb2D;
    public GameController gameController;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveForce)
    {
        Vector2 moveForceVector = new Vector2(0.0f, moveForce * carSpeed);
        rb2D.AddRelativeForce(moveForceVector);
        StartCoroutine("startDynamicDrag");
    }

    public IEnumerator startDynamicDrag()
    {
        float defaultDrag = rb2D.drag;
        rb2D.drag = 0;

        yield return null;  //Waiting for the velocity to apply
        yield return new WaitForSeconds(firstSlowTime);
        while (rb2D.velocity.magnitude >= minMovingSpeed)
        {
            Debug.Log("Did it even work?");
            yield return new WaitForSeconds(deltaDragTime);
            rb2D.drag += deltaDragPower;
        }
        rb2D.drag = defaultDrag;
        
    }

    public void SwipeAction(string direction)
    {
        
        if (direction == "left")
        {
            rb2D.AddTorque(turnPower);
            StartCoroutine("maintainVelocityRotation");
        }
        else if (direction == "right")
        {

            rb2D.AddTorque(-turnPower);
            StartCoroutine("maintainVelocityRotation");
            //Quaternion rotation = new Quaternion(0.0f, 0.0f, rb2D.transform.rotation.z - turnDegrees, 0.0f);
            //Debug.Log(rb2D.transform.rotation.z - turnDegrees);
            //rb2D.transform.rotation = rotation;
        }
    }

    private void Update()
    {
        if(Input.GetButton("Jump")){
            Move(carSpeed);
        }

    }

    public IEnumerator maintainVelocityRotation()
    {
        while(rb2D.velocity.magnitude > minMovingSpeed)
        {

            Vector3 velocity = rb2D.velocity;
            rb2D.velocity = rb2D.transform.up * velocity.magnitude;
            //rb2D.velocity = transform.forward * velocity.magnitude;
            yield return new WaitForFixedUpdate();
        }
        
    }
    
    public IEnumerator startNextTurnWhenStopped()
    {
        yield return null;      // Because coroutine ended up being called before any actual movement was applied resulting in bugs xd

        while (rb2D.velocity.magnitude >= minMovingSpeed)
        {
            yield return new WaitForFixedUpdate();
        }

        rb2D.velocity = new Vector2(0.0f, 0.0f);
        gameController.StartAimingTurn();

    }
}
