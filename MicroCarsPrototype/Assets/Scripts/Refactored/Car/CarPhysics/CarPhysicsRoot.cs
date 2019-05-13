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
    protected const float minMovingSpeed = 0.001f;
    protected const float movingSpeedSlowDownValue = minMovingSpeed * 1000;

    protected CarPhysicsMovement carMovement;
    protected CarPhysicsDynamicDrag carDynamicDrag;
    protected CarPhysicsTurning carTurning;
    protected CarPhysicsBraking carBraking;
    protected Rigidbody2D rb2D;
    protected GameController gameController;

    protected bool stateIsMoving = false;

    private float defaultAngularDrag;

    // Start is called before the first frame update
    void Start()
    {
        carMovement = GetComponent<CarPhysicsMovement>();
        carDynamicDrag = GetComponent<CarPhysicsDynamicDrag>();
        carTurning = GetComponent<CarPhysicsTurning>();
        carBraking = GetComponent<CarPhysicsBraking>();
        rb2D = GetComponent<Rigidbody2D>();

        defaultAngularDrag = rb2D.angularDrag;
    }

    public void InitializeMovement(float moveForce)
    {
        carMovement.Move(moveForce);
    }

    public IEnumerator StartNextTurnWhenStopped()
    {
        yield return new WaitForSeconds(1);      // Because coroutine ended up being called before any actual movement was applied resulting in bugs xd

        while (rb2D.velocity.magnitude >= minMovingSpeed)
        {
            yield return new WaitForFixedUpdate();
        }

        rb2D.angularDrag = defaultAngularDrag + 1.0f;
        rb2D.angularVelocity = 3.0f;
        rb2D.velocity = new Vector2(0.0f, 0.0f);
        yield return null;                      // So that the angularVelocity is applied for sure before we change the game state.


        stateIsMoving = false;
        //gameController.switchTurnState(false);
        //gameController.StartAimingTurn();

    }
}
