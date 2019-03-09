﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour {

    private const float minMovingSpeed = 0.01f;

    private bool isMoving = false;
    public float carSpeed = 0;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveForce)
    {

        rb2D.velocity.Set(0, 0);
        Vector2 moveForceVector = new Vector2(0.0f, moveForce * carSpeed);
        rb2D.AddRelativeForce(moveForceVector);
    }


    private void Update()
    {
        if(Input.GetButton("Jump")){
            Move(10);
        }

    }
}
