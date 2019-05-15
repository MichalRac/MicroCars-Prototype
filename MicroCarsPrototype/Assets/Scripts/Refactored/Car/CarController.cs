using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTurnEndCallback();

[RequireComponent(typeof(CarPhysicsRoot))]
[RequireComponent(typeof(AimingRoot))]
[RequireComponent(typeof(CarStates))]
public class CarController : MonoBehaviour
{

    private CarPhysicsRoot carPhysics;
    private AimingRoot aiming;

    public static OnTurnEndCallback onTurnEnd;

    // Start is called before the first frame update
    void Start()
    {
        carPhysics = GetComponent<CarPhysicsRoot>();
        aiming = GetComponent<AimingRoot>();

        onTurnEnd = () => Debug.Log("TurnFinished");

        StartCarTurn();
    }

    public void StartCarTurn()
    {
        Debug.Log("AimingStarted");
        aiming.AimStart(onTurnEnd);
    }

}
