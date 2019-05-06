using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsMovement : CarPhysicsRoot
{

    [SerializeField]
    private float carSpeed = 0.0f;
    [SerializeField]
    private float carMaxVelocity = 7.5f;

    
    public void Move(float moveForce)
    {
        Vector2 moveForceVector = new Vector2(0.0f, moveForce * carSpeed);
        base.stateIsMoving = true;
        rb2D.AddRelativeForce(moveForceVector);

        StartCoroutine(base.carDynamicDrag.startDynamicDrag());

        StartCoroutine(maintainVelocityRotation());
    }

    public IEnumerator maintainVelocityRotation()
    {
        while (rb2D.velocity.magnitude > minMovingSpeed)
        {

            Vector3 velocity = rb2D.velocity;
            rb2D.velocity = rb2D.transform.up * velocity.magnitude;
            //rb2D.velocity = transform.forward * velocity.magnitude;
            yield return new WaitForFixedUpdate();
        }

    }

}
