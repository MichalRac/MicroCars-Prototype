using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsBraking : CarPhysicsRoot
{
    [SerializeField]
    private float breaksPower = 3.0f;
    private CarStates carStates;

    private void Start()
    {
        carStates = GetComponent<CarStates>();
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

}
