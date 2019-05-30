using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarPhysicsMovement))]
[RequireComponent(typeof(CarPhysicsDynamicDrag))]
[RequireComponent(typeof(CarPhysicsTurning))]
[RequireComponent(typeof(CarPhysicsBraking))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AimingRoot))]
public class CarPhysicsRoot : MonoBehaviour
{
    private const float _minMovingSpeed = 0.001f;
    private const float _movingSpeedSlowDownValue = _minMovingSpeed * 1000;
    private float defaultAngularDrag;   // Drag we have on Awake

    public static float MinMovingSpeed => _minMovingSpeed;
    public static float MovingSpeedSlowDownValue => _movingSpeedSlowDownValue;

    private CarPhysicsMovement carMovement;
    private CarPhysicsDynamicDrag carDynamicDrag;
    private CarPhysicsTurning carTurning;
    private CarPhysicsBraking carBraking;
    private Rigidbody2D rb2D;
    private CarStates states;

    OnMovementFinishedCallback onMovementFinishedCallbackReference;
    public OnMovementFinishedCallback OnMovementFinishedCallbackReference { get => onMovementFinishedCallbackReference; set => onMovementFinishedCallbackReference = value; }



    private void Awake()
    {
        carMovement = GetComponent<CarPhysicsMovement>();
        carDynamicDrag = GetComponent<CarPhysicsDynamicDrag>();
        carTurning = GetComponent<CarPhysicsTurning>();
        carBraking = GetComponent<CarPhysicsBraking>();
        rb2D = GetComponent<Rigidbody2D>();
        states = GetComponent<CarStates>();
        Debug.Assert(carMovement, $"{typeof(CarPhysicsMovement)} is null");
        Debug.Assert(carDynamicDrag, $"{typeof(CarPhysicsDynamicDrag)} is null");
        Debug.Assert(carTurning, $"{typeof(CarPhysicsTurning)} is null");
        Debug.Assert(carBraking, $"{typeof(CarPhysicsBraking)} is null");
        Debug.Assert(rb2D, $"{typeof(Rigidbody2D)} is null");
        Debug.Assert(states, $"{typeof(CarStates)} is null");

        carMovement.MinMovingSpeed = _minMovingSpeed;
        carDynamicDrag.MovingSpeedSlowDownValue = MovingSpeedSlowDownValue;
        carDynamicDrag.MinMovingSpeed = MinMovingSpeed;
        carDynamicDrag.rb2D = rb2D;
        defaultAngularDrag = rb2D.angularDrag;
    }

    public void InitializeMovement(float moveForce)
    {
        states.IsMoving = true;
        carMovement.Move(moveForce);
    }

    public void InitializeSwipe(string direction, float lenght, Vector3 swipeVector)
    {
        carTurning.SwipeAction(direction, lenght, swipeVector);
    }

    public IEnumerator StartNextTurnWhenStopped()
    {
        Debug.Log("StartNextTurnWhenStopped initialized");
        yield return new WaitForSeconds(1);      // Because coroutine ended up being called before any actual movement was applied resulting in bugs

        while (rb2D.velocity.magnitude >= _minMovingSpeed)
        {
            yield return new WaitForFixedUpdate();
        }

        rb2D.angularDrag = defaultAngularDrag + 1.0f;
        rb2D.angularVelocity = 3.0f;
        rb2D.velocity = new Vector2(0.0f, 0.0f);
        yield return null;                      // So that the angularVelocity is applied for sure before we change the game state.


        states.IsMoving = false;
        OnMovementFinishedCallbackReference();
        //gameController.switchTurnState(false);
        //gameController.StartAimingTurn();

    }
}
