using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsDynamicDrag : CarPhysicsRoot
{
    [Header("Dynamic drag options")]
    [SerializeField]
    private float deltaDragPower = 0.5f;
    [SerializeField]
    private float deltaDragTime = 0.5f;
    [SerializeField]
    private float firstSlowTimeDelay = 0.5f;
    [SerializeField]
    private float surfaceDragModifiers = 1;

    public IEnumerator StartDynamicDrag()
    {
        float defaultDrag = rb2D.drag;
        bool gotBelowSlowDownValue = false;
        rb2D.drag = 0.0f;
        rb2D.angularDrag = 0.0f;

        yield return null;  //Waiting for the velocity to apply
        yield return new WaitForSeconds(firstSlowTimeDelay);
        while (rb2D.velocity.magnitude >= minMovingSpeed)
        {
            yield return new WaitForSeconds(deltaDragTime);

            if (rb2D.velocity.magnitude <= movingSpeedSlowDownValue && gotBelowSlowDownValue == false)    // Hardcoding low value of drag and angularDrag when slowing down.
            {
                gotBelowSlowDownValue = true;
                rb2D.drag += 2.0f;
                rb2D.angularDrag += 2.0f;
            }
            if (rb2D.velocity.magnitude > movingSpeedSlowDownValue && gotBelowSlowDownValue == true)    // Reversing the check above if a case appears where we speed up back above slow down value
            {
                gotBelowSlowDownValue = false;
                rb2D.drag -= 2.0f;
                rb2D.angularDrag -= 2.0f;
            }

            rb2D.drag += deltaDragPower * surfaceDragModifiers;
        }
        rb2D.drag = defaultDrag;

    }
}
