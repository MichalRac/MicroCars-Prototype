using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarPhysicsMovement))]
[RequireComponent(typeof(CarPhysicsDynamicDrag))]
[RequireComponent(typeof(CarPhysicsTurning))]
[RequireComponent(typeof(CarPhysicsBraking))]
[RequireComponent(typeof(Rigidbody2D))]
public class CarPhysicsRoot : MonoBehaviour
{
    protected const float minMovingSpeed = 0.001f;

    protected CarPhysicsMovement carMovement;
    protected CarPhysicsDynamicDrag carDynamicDrag;
    protected CarPhysicsTurning carTurning;
    protected CarPhysicsBraking carBraking;
    protected Rigidbody2D rb2D;

    protected bool stateIsMoving = false;



    // Start is called before the first frame update
    void Start()
    {
        carMovement = GetComponent<CarPhysicsMovement>();
        carDynamicDrag = GetComponent<CarPhysicsDynamicDrag>();
        carTurning = GetComponent<CarPhysicsTurning>();
        carBraking = GetComponent<CarPhysicsBraking>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void initializeMovement(float moveForce)
    {
        carMovement.Move(moveForce);
    }




}
