using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsBraking : MonoBehaviour
{
    [SerializeField]
    private float breaksPower = 3.0f;
    private CarStates carStates;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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
