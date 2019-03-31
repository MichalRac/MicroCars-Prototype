using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour {

    private const float minMovingSpeed = 0.01f;

    private bool isMoving = false;
    public float carSpeed = 0.0f;
    public float turnPower = 5.0f;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveForce)
    {


        Vector2 moveForceVector = new Vector2(0.0f, moveForce * carSpeed);
        rb2D.AddRelativeForce(moveForceVector);
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
}
