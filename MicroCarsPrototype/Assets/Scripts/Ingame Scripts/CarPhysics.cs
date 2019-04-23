using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour {

    private const float minMovingSpeed = 0.001f;
    private bool isMoving = false;
    private Rigidbody2D rb2D;
    private float defaultAngularDrag;

    [Header("Speed and swipe turning options")]
    public float carSpeed = 0.0f;
    public float turnPower = 5.0f;
    public float turnSwipeResponsivness = 1.0f;
    public float breaksPower = 3.0f;

    [Header("Dynamic drag options")]
    public float deltaDragPower = 0.5f;
    public float deltaDragTime = 0.5f;
    public float firstSlowTime = 0.5f;
    public float surfaceDragModifiers = 1;

    [Header("Referenced Scripts")]
    private GameController gameController;


    // TODO?: REWORK ALL THOSE GOD FORSAKEN COROUTINES INTO INVOKES?


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        gameController = FindObjectOfType<GameController>();
        defaultAngularDrag = rb2D.angularDrag;
    }

    public void Move(float moveForce)
    {
        isMoving = true;
        Vector2 moveForceVector = new Vector2(0.0f, moveForce * carSpeed);
        rb2D.AddRelativeForce(moveForceVector);
        StartCoroutine("startDynamicDrag");
        StartCoroutine("maintainVelocityRotation");
    }

    public IEnumerator startDynamicDrag()
    {
        float defaultDrag = rb2D.drag;
        rb2D.drag = 0.0f;

        yield return null;  //Waiting for the velocity to apply
        yield return new WaitForSeconds(firstSlowTime);
        while (rb2D.velocity.magnitude >= minMovingSpeed)
        {
            yield return new WaitForSeconds(deltaDragTime);
            rb2D.drag += deltaDragPower * surfaceDragModifiers;
        }
        rb2D.drag = defaultDrag;
        
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


    public void SwipeAction(string direction, float swipeLenght)
    {
        if (gameController.getIsPlayerTurn() == true)
            return;
        
        if (direction == "left")
        {
            rb2D.AddTorque(turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine("maintainVelocityRotation");
        }

        else if (direction == "right")
        {
            rb2D.AddTorque(-turnPower /* * (swipeLenght * turnSwipeResponsivness) */);
            StartCoroutine("maintainVelocityRotation");
        }
    }
    

    public IEnumerator startNextTurnWhenStopped()
    {
        yield return new WaitForSeconds(1);      // Because coroutine ended up being called before any actual movement was applied resulting in bugs xd

        while (rb2D.velocity.magnitude >= minMovingSpeed)
        {
            yield return new WaitForFixedUpdate();

            if (rb2D.velocity.magnitude >= 2.0f)
                rb2D.angularDrag = 3.0f;
        }
        rb2D.angularDrag = defaultAngularDrag;
        rb2D.angularVelocity = 0;
        rb2D.velocity = new Vector2(0.0f, 0.0f);
        yield return null;                      // So that the angularVelocity is applied for sure before we change the game state.

        isMoving = false;
        gameController.switchTurnState(false);
        gameController.StartAimingTurn();

    }



    public void triggerCustomTurnEffect(float turnDurationSec, float targetAngle)
    {
        StartCoroutine(customTurnEffect(turnDurationSec, targetAngle));
    }

    //TODO: Make the turn happen over precise seconds set in turnDurationSec
    public IEnumerator customTurnEffect(float turnDurationSec, float targetAngle)
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


    public void breaksOn()
    {
        rb2D.drag += breaksPower;
        rb2D.angularDrag += breaksPower;
    }

    public void breaksOff()
    {
        rb2D.drag -= breaksPower;
        rb2D.angularDrag -= breaksPower;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Collider2D>() != null)
        {
            rb2D.angularDrag += 3.0f;
            Invoke("resetAngularDrag", 1.0f);
            
        }
    }

    private void resetAngularDrag()
    {
        rb2D.angularDrag = defaultAngularDrag;
    }


}
