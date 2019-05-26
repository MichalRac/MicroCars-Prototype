using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsBraking : MonoBehaviour
{
    [SerializeField]
    private float breaksPower = 3.0f;
    private CarStates carStates;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        carStates = GetComponent<CarStates>();
        Debug.Assert(rb2D, $"{typeof(Rigidbody2D)} is null");
        Debug.Assert(carStates, $"{typeof(CarStates)} is null");
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
