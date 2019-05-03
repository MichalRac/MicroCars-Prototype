using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsDynamicDrag : CarPhysicsRoot
{
    [Header("Dynamic drag options")]
    public float deltaDragPower = 0.5f;
    public float deltaDragTime = 0.5f;
    public float firstSlowTimeDelay = 0.5f;
    public float surfaceDragModifiers = 1;

    public IEnumerator startDynamicDrag()
    {
        float defaultDrag = rb2D.drag;
        rb2D.drag = 0.0f;
        rb2D.angularDrag = 0.0f;

        yield return null;  //Waiting for the velocity to apply
        yield return new WaitForSeconds(firstSlowTimeDelay);
        while (rb2D.velocity.magnitude >= minMovingSpeed)
        {
            yield return new WaitForSeconds(deltaDragTime);
            rb2D.drag += deltaDragPower * surfaceDragModifiers;
        }
        rb2D.drag = defaultDrag;

    }
}
